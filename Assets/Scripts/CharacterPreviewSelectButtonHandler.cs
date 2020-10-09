using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewSelectButtonHandler : MonoBehaviour
{
    private AudioSource _buttonClick;

    public GameObject parentCharacterPreviewCanvas;
    
    private void Start()
    {
        _buttonClick = gameObject.GetComponent<AudioSource>();
    }

    public void OnCharacterPreviewSelectButton()
    {
        _buttonClick.Play();
        
        // don't show character preview description canvas
        CharacterPreview characterPreview = GetThisCharacterPreview();
        characterPreview.StopShowingCharacterDescription();
        
        // start prepare room for counsel phase
        GameManager.Instance.PrepareARoomForTheKingsCounsel();
    }

    private CharacterPreview GetThisCharacterPreview()
    {
        GameObject characterObject = parentCharacterPreviewCanvas.transform.parent.gameObject;
        
        // sift through a list of child objects to find the specific one
        for (var i = 0; i < characterObject.transform.childCount; i++)
        {
            // object is not what we're looking for
            if (characterObject.transform.GetChild(i).gameObject.name !=
                "CharacterPreviewComponents") continue;
            
            // found the game object we're looking for
            return characterObject.transform.GetChild(i).gameObject.GetComponent<CharacterPreview>();
        }

        return null;
    }
}
