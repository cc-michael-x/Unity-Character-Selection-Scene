using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewSelectButtonHandler : MonoBehaviour
{
    private AudioSource _buttonClick;

    private void Start()
    {
        _buttonClick = gameObject.GetComponent<AudioSource>();
    }

    public void OnCharacterPreviewSelectButton()
    {
        _buttonClick.Play();
    }
}
