using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    private const string CharacterPreviewComponents = "CharacterPreviewComponents";
    public static GameManager Instance;

    [Header("Phases")] 
    public bool _characterSelectionPhase;
    public bool _characterPreviewPhase;
    public bool _prepareCounselPhase;
    public bool _prepareCounselUIOpened;
    
    [Header("Cameras")]
    public CinemachineVirtualCamera initialMenuSetupVirtualCamera;
    public CinemachineVirtualCamera followUpMenuVirtualCamera;
    public CinemachineFreeLook defaultCmFreeLookCam;
    public CinemachineVirtualCamera kingsCounselVCam;
    
    [Header("Characters")]
    public string currentCharacterSelected;
    public GameObject characters;
    public GameObject[] charactersArray;

    [Header("UI Config")]
    public GameObject fadeInImage;
    public GameObject fadeOutImage;
    private bool _fadingInImage;
    private float _fadingInImageTime = 1f;
    private bool _fadingOutImage;
    private float _fadingOutImageTime = 2f;
    private float _initialMenuSetupVirtualCameraTime = 3f;
    public Texture2D defaultCursor;
    public GameObject moveCameraMouseTipCanvas;
    public GameObject prepareCounselUI;

    [Header("Music")] 
    public AudioSource mainMusic;
    public AudioSource menuSoundButton;
    
    [Header("Animators")]
    public Animator theDoorsAnimator;
    private static readonly int StartDoorAnimation = Animator.StringToHash("StartDoorAnimation");
    
    private bool _blockSelect;
    private float _blockSelectTime;
    private const float BlockSelectTimeConst = 1f;
    
    private void Start()
    {
        Instance = this;

        // fade into scene
        EnterPrepareCounselScene();
        
        // start with character selection phase
        _characterSelectionPhase = true;
        
        // enable main lobby free look cam
        defaultCmFreeLookCam.enabled = true;
        
        // disable vcam prepare counsel room camera
        kingsCounselVCam.enabled = false;
        
        // dont show canvas by default
        prepareCounselUI.SetActive(false);
        
        // set default cursor
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        
        // dynamically fetch character list from parent character game object
        charactersArray = new GameObject[characters.transform.childCount];
        for (var i = 0; i < characters.transform.childCount; i++)
        {
            for (var x = 0; x < characters.transform.GetChild(i).transform.childCount; x++)
            {
                if (characters.transform.GetChild(i).GetChild(x).gameObject.name !=
                    CharacterPreviewComponents) continue;
                charactersArray[i] = characters.transform.GetChild(i).GetChild(x).gameObject;
                x = characters.transform.GetChild(i).transform.childCount;
            }
        }
        
        // confine the cursor to the screen
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if (_fadingInImage)
        {
            _fadingInImageTime -= Time.deltaTime;
            if (_fadingInImageTime <= 0)
            {
                fadeInImage.SetActive(false);
                _fadingInImage = false;
            }
        }
        
        if (_fadingOutImage)
        {
            _initialMenuSetupVirtualCameraTime -= Time.deltaTime;

            if (_initialMenuSetupVirtualCameraTime <= 0.5f)
            {
                initialMenuSetupVirtualCamera.enabled = false;
                followUpMenuVirtualCamera.enabled = true;
            }

            if (_initialMenuSetupVirtualCameraTime <= 0)
            {
                _fadingOutImageTime -= Time.deltaTime;
                mainMusic.volume -= Time.deltaTime / 10;

                StartAnimationLeaveToMenuScene();
                    
                if (_fadingOutImageTime <= 0)
                {
                    SceneManager.LoadScene("Menu");
                    _fadingOutImage = false;
                }
            }
        }
        
        // stop showing move camera tooltip
        if(Input.GetMouseButton(1))
            moveCameraMouseTipCanvas.SetActive(false);

        // show the cursor when the user releases the right mouse button
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        // Wait until waitTime is below or equal to zero.
        if(_blockSelectTime > 0) {
            _blockSelectTime -= Time.deltaTime;
        } else {
            // Done.
            _blockSelect = false;
        }
        
        if (Input.GetKeyDown("escape"))
        {
            if (_blockSelect)
                return;
            
            if (_prepareCounselPhase)
            {
                QuitPreparingARoomForTheKingsCounsel();
                _prepareCounselPhase = false;
                _characterSelectionPhase = false;
                _characterPreviewPhase = true;
                _prepareCounselUIOpened = false;
            }
            else if (_characterSelectionPhase)
            {
                prepareCounselUI.SetActive(true);
                _prepareCounselPhase = false;
                _characterSelectionPhase = false;
                _characterPreviewPhase = false;
                _prepareCounselUIOpened = true;
            }
            else if (_prepareCounselUIOpened)
            {
                prepareCounselUI.SetActive(false);
                _prepareCounselPhase = false;
                _characterSelectionPhase = true;
                _characterPreviewPhase = false;
                _prepareCounselUIOpened = false;
            }
        }
    }
    
    public void BlockEscapeKey()
    {
        _blockSelect = true; // block the input
        _blockSelectTime = BlockSelectTimeConst;
    }

    private void EnterPrepareCounselScene()
    {
        _fadingInImage = true;
        fadeInImage.SetActive(true);
    }

    // disable all other character preview components that isn't the currently selected character preview
    public void SetCurrentCharacterPreview(string characterName)
    {
        if (!characterName.IsNullOrEmpty())
        {
            currentCharacterSelected = characterName;
            _characterPreviewPhase = true;
            _characterSelectionPhase = false;
        }
        else
        {
            _characterPreviewPhase = false;
            _characterSelectionPhase = true;
        }

        BlockEscapeKey();
        
        GameObject[] nonSelectedCharacterPreviews = 
            charactersArray.Where(character => 
                character.GetComponent<CharacterPreview>().characterName != characterName).ToArray();

        foreach (var nonSelectedCharacterPreview in nonSelectedCharacterPreviews)
        {
            nonSelectedCharacterPreview.GetComponent<CharacterPreview>().enabled = characterName.IsNullOrEmpty();
        }
    }

    public void PrepareARoomForTheKingsCounsel()
    {
        _characterPreviewPhase = false;
        _prepareCounselPhase = true;
        kingsCounselVCam.enabled = true;
    }

    private void QuitPreparingARoomForTheKingsCounsel()
    {
        _characterPreviewPhase = true;
        _prepareCounselPhase = false;
        
        kingsCounselVCam.enabled = false;
        
        // get the character that was selected before entering the prepare phase
        GameObject characterThatWasSelected = charactersArray.First(character =>
            character.transform.parent.gameObject.name == currentCharacterSelected);
            
        CharacterPreview characterPreview = characterThatWasSelected.GetComponent<CharacterPreview>();
            
        characterPreview.enabled = true;
        characterPreview.EnterCharacterPreview();
    }

    public void DontQuitMenuSceneButton()
    {
        menuSoundButton.Play();
        
        prepareCounselUI.SetActive(false);
        _prepareCounselPhase = false;
        _characterSelectionPhase = true;
        _characterPreviewPhase = false;
        _prepareCounselUIOpened = false;
    }
    
    public void QuitMenuSceneButton()
    {
        menuSoundButton.Play();

        // dont show menu ui
        prepareCounselUI.SetActive(false);
        
        // switch cameras to go towards door
        defaultCmFreeLookCam.enabled = false;
        initialMenuSetupVirtualCamera.enabled = true;

        _fadingOutImage = true;
    }

    private void StartAnimationLeaveToMenuScene()
    {
        // start door animation
        theDoorsAnimator.SetBool(StartDoorAnimation, true);
        fadeOutImage.SetActive(true);
    }
} 
