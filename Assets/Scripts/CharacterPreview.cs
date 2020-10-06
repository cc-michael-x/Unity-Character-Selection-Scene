using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public GameObject cmFreeLookCam;
    public GameObject cmFreeLookPoint;

    private CharacterHeadLookAtCamera _characterHeadLookAtCamera;
    private Animator _animator;
    private static readonly int Preview = Animator.StringToHash("CharacterPreview");

    private void Start()
    {
        _characterHeadLookAtCamera = gameObject.GetComponent<CharacterHeadLookAtCamera>();
        _animator = gameObject.GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        _characterHeadLookAtCamera.enabled = true;
        
        cmFreeLookCam.GetComponent<CinemachineFreeLook>().Follow = cmFreeLookPoint.transform;
        cmFreeLookCam.GetComponent<CinemachineFreeLook>().LookAt = cmFreeLookPoint.transform;
        
        _animator.SetBool(Preview, true);
    }
}
