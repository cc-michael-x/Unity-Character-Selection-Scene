using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using HighlightPlus;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterPreview : MonoBehaviour
{
    [Header("Character Description")]
    [Tooltip("The characterName will be automatically set using the name of the root game object of the character")]
    public string characterName;
    [Multiline]
    public string characterNameDescriptionText;
    public GameObject characterPreviewCanvas;
    public GameObject characterDescriptionParent;
    
    [Header("Character Description Components")]
    private TextMeshProUGUI _characterNameText;
    private TextMeshProUGUI _characterNameDescription;
    private const string CharacterNameTextObjectName = "CharacterNameText";
    private const string CharacterDescriptionTextObjectName = "CharacterDescriptionText";
    
    [Header("Cameras")]
    public GameObject defaultCmFreeLookCam;
    public GameObject characterCmFreeLookCam;

    [Header("Highlight Effects")] 
    public HighlightProfile glowingHighlightProfile;
    public HighlightProfile triggerHighlightProfile;
    private HighlightEffect _triggerHighlightEffect;
    private HighlightTrigger _highlightTrigger;

    [Header("Cursor Images")] 
    public Texture2D defaultCursor;
    public Texture2D characterPreviewHoverCursor;
    public Texture2D characterPreviewHoverExitCursor;
    
    [Header("Sounds")]
    public AudioSource characterPreviewOnOnClickSound;
    public AudioSource characterPreviewOffOnClickSound;
    public AudioSource bookSound;
    public AudioSource characterPreviewHoverEnterSound;
    public AudioSource characterPreviewHoverExitSound;
    
    private bool _characterPreview;
    private CinemachineFreeLook _cmDefaultFreeLook;
    private CinemachineVirtualCamera _cmCharacterFreeLook;
    private CharacterHeadLookAtCamera _characterHeadLookAtCamera;
    private Animator _animator;
    private static readonly int Preview = Animator.StringToHash("CharacterPreview");

    private void Start()
    {
        // get highlight components for character
        _triggerHighlightEffect = gameObject.GetComponent<HighlightEffect>();
        _highlightTrigger = gameObject.GetComponent<HighlightTrigger>();
        
        // handle initial highlight characters setup
        _triggerHighlightEffect.profile = glowingHighlightProfile;
        _triggerHighlightEffect.ProfileReload();
        _triggerHighlightEffect.highlighted = true;
        _highlightTrigger.enabled = false;
        
        // get TextMeshProUGUI character description objects
        _characterNameText = FindGameObjectInChildren(characterDescriptionParent, CharacterNameTextObjectName);
        _characterNameDescription = FindGameObjectInChildren(characterDescriptionParent, CharacterDescriptionTextObjectName);
        
        // set character preview description
        characterName = transform.parent.gameObject.name;
        _characterNameText.text = characterName;
        _characterNameDescription.text = characterNameDescriptionText;
        
        _characterHeadLookAtCamera = gameObject.GetComponent<CharacterHeadLookAtCamera>();
        _animator = gameObject.GetComponent<Animator>();
        _cmDefaultFreeLook = defaultCmFreeLookCam.GetComponent<CinemachineFreeLook>();
        _cmCharacterFreeLook = characterCmFreeLookCam.GetComponent<CinemachineVirtualCamera>();
    }

    // the mouse state is locked when the user is moving the camera while holding down the right mouse button
    private bool IsTheMouseStateLocked()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }
    
    private void OnMouseEnter()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
            return;*/
        
        if (IsTheMouseStateLocked())
            return;
        
        characterPreviewHoverEnterSound.Play();
    }

    // handle highlighting the characters when we hover over the characters anymore
    private void OnMouseOver()
    {
        /*if (EventSystem.current.IsPointerOverGameObject())
            return;*/
        
        if (IsTheMouseStateLocked())
            return;
        
        _highlightTrigger.enabled = true;
        _triggerHighlightEffect.profile = triggerHighlightProfile;
        _triggerHighlightEffect.ProfileReload();
        
        if(!_characterPreview)
            Cursor.SetCursor(characterPreviewHoverCursor, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(characterPreviewHoverExitCursor, Vector2.zero, CursorMode.Auto);
    }

    // handle highlighting the characters when we don't hover over the characters anymore
    private void OnMouseExit()
    {
        if (IsTheMouseStateLocked())
            return;
        
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        
        characterPreviewHoverExitSound.Play();
        
        _highlightTrigger.enabled = false;
        _triggerHighlightEffect.profile = glowingHighlightProfile;
        _triggerHighlightEffect.ProfileReload();
        _triggerHighlightEffect.highlighted = true;
    }

    void OnMouseDown()
    {
        if (!enabled)
            return;
        
        if (IsTheMouseStateLocked())
            return;

        /*if (EventSystem.current.IsPointerOverGameObject())
            return;*/
        
        if (!_characterPreview)
        {
            // set this character as the current character preview
            GameManager.Instance.SetCurrentCharacterPreview(characterName);
            
            // character preview is now ON for this character
            _characterPreview = true;
            
            // play on select click sound
            characterPreviewOnOnClickSound.Play();
            bookSound.Play();

            // enable the free look vcam for this character
            _characterHeadLookAtCamera.enabled = true;

            // disable the default free look vcam
            _cmDefaultFreeLook.enabled = false;
            // enable the free look vcam for this character
            _cmCharacterFreeLook.enabled = true;
            
            characterPreviewCanvas.SetActive(true);
        }
        else
        {
            // deselect this character as the current character preview
            GameManager.Instance.SetCurrentCharacterPreview("");
            
            // character preview is now OFF for this character
            _characterPreview = false;
            
            // play on deselect click sound
            characterPreviewOffOnClickSound.Play();
            
            // disable the free look vcam for this character
            _characterHeadLookAtCamera.enabled = false;

            // enable the default free look vcam
            _cmDefaultFreeLook.enabled = true;
            // disable the free look vcam for this character
            _cmCharacterFreeLook.enabled = false;
            
            characterPreviewCanvas.SetActive(false);
        }
    }

    // return a TextMeshProUGUI GameObject that was found in the list of child objects in parent object
    private static TextMeshProUGUI FindGameObjectInChildren(GameObject parentGameObject, string nameOfGameObjectToFind)
    {
        // sift through a list of child objects to find the specific one
        for (var i = 0; i < parentGameObject.transform.childCount; i++)
        {
            // object is not what we're looking for
            if (parentGameObject.transform.GetChild(i).gameObject.name !=
                nameOfGameObjectToFind) continue;
            
            // found the game object we're looking for
            return parentGameObject.transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
        }

        return null;
    }
}
