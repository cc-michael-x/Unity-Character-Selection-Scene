using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject defaultCmFreeLookCam;
    public GameObject characterCmFreeLookCam;
    
    [Header("Sounds")]
    public AudioSource characterPreviewOnOnClickSound;
    public AudioSource characterPreviewOffOnClickSound;
    
    private bool _characterPreview = false;
    private CinemachineFreeLook _cmDefaultFreeLook;
    private CinemachineFreeLook _cmCharacterFreeLook;
    private CharacterHeadLookAtCamera _characterHeadLookAtCamera;
    private Animator _animator;
    private static readonly int Preview = Animator.StringToHash("CharacterPreview");

    private void Start()
    {
        _characterHeadLookAtCamera = gameObject.GetComponent<CharacterHeadLookAtCamera>();
        _animator = gameObject.GetComponent<Animator>();
        _cmDefaultFreeLook = defaultCmFreeLookCam.GetComponent<CinemachineFreeLook>();
        _cmCharacterFreeLook = characterCmFreeLookCam.GetComponent<CinemachineFreeLook>();
    }

    void OnMouseDown()
    {
        if (!_characterPreview)
        {
            _characterPreview = true;
            
            characterPreviewOnOnClickSound.Play();

            _characterHeadLookAtCamera.enabled = true;

            _cmDefaultFreeLook.enabled = false;
            _cmCharacterFreeLook.enabled = true;
        }
        else
        {
            _characterPreview = false;
            
            characterPreviewOffOnClickSound.Play();
            
            _characterHeadLookAtCamera.enabled = false;

            _cmDefaultFreeLook.enabled = true;
            _cmCharacterFreeLook.enabled = false;
        }
    }
}
