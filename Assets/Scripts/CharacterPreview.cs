using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

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
    
    [Header("Sounds")]
    public AudioSource characterPreviewOnOnClickSound;
    public AudioSource characterPreviewOffOnClickSound;
    public AudioSource bookSound;
    
    private bool _characterPreview;
    private CinemachineFreeLook _cmDefaultFreeLook;
    private CinemachineFreeLook _cmCharacterFreeLook;
    private CharacterHeadLookAtCamera _characterHeadLookAtCamera;
    private Animator _animator;
    private static readonly int Preview = Animator.StringToHash("CharacterPreview");

    private void Start()
    {
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
        _cmCharacterFreeLook = characterCmFreeLookCam.GetComponent<CinemachineFreeLook>();
    }

    void OnMouseDown()
    {
        if (!enabled)
            return;
        
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
