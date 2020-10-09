using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    private const string CharacterPreviewComponents = "CharacterPreviewComponents";
    public static GameManager Instance;

    [Header("Phases")] 
    private bool _characterSelectionPhase;
    private bool _prepareCounselPhase;
    
    [Header("Cameras")]
    public GameObject defaultFreeLookCam;
    public GameObject theKingsCounselVCam;
    private CinemachineFreeLook _defaultCmFreeLookCam;
    private CinemachineVirtualCamera _kingsCounselVCam;
    
    [Header("Characters")]
    public string currentCharacterSelected;
    public GameObject characters;
    public GameObject[] charactersArray;

    [Header("UI Config")]
    public Texture2D defaultCursor;
    public GameObject moveCameraMouseTipCanvas;

    private void Start()
    {
        Instance = this;

        // start with character selection phase
        _characterSelectionPhase = true;
        
        // enable main lobby free look cam
        _defaultCmFreeLookCam = defaultFreeLookCam.GetComponent<CinemachineFreeLook>();
        _defaultCmFreeLookCam.enabled = true;
        
        // disable vcam prepare counsel room camera
        _kingsCounselVCam = theKingsCounselVCam.GetComponent<CinemachineVirtualCamera>();
        _kingsCounselVCam.enabled = false;
        
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
        // stop showing move camera tooltip
        if(Input.GetMouseButton(1))
            moveCameraMouseTipCanvas.SetActive(false);

        // show the cursor when the user releases the right mouse button
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        if (_prepareCounselPhase && Input.GetKey("escape"))
        {
            QuitPreparingARoomForTheKingsCounsel();
        }
    }

    public bool IsItCharacterPreviewPhase()
    {
        return _characterSelectionPhase;
    }
    
    // disable all other character preview components that isn't the currently selected character preview
    public void SetCurrentCharacterPreview(string characterName)
    {
        if (!characterName.IsNullOrEmpty())
            currentCharacterSelected = characterName;
        
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
        _characterSelectionPhase = false;
        _prepareCounselPhase = true;
        theKingsCounselVCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
    }

    private void QuitPreparingARoomForTheKingsCounsel()
    {
        _prepareCounselPhase = false;
        _characterSelectionPhase = true;

        _kingsCounselVCam.enabled = false;

        // get the character that was selected before entering the prepare phase
        GameObject characterThatWasSelected = charactersArray.First(character =>
            character.transform.parent.gameObject.name == currentCharacterSelected);
            
        CharacterPreview characterPreview = characterThatWasSelected.GetComponent<CharacterPreview>();
            
        characterPreview.enabled = true;
        characterPreview.EnterCharacterPreview();
    }
} 
