using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    [Header("Config")]
    public string characterName;
    public GameObject characterPreviewCanvas;
    public TextMeshProUGUI characterNameText;
    
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
        characterName = transform.parent.gameObject.name;
        characterNameText.text = characterName;
        
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
}
