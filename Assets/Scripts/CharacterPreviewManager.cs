using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewManager : MonoBehaviour
{
    private CharacterPreview _characterPreview;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterPreview = gameObject.GetComponent<CharacterPreview>();
    }

    private void Update()
    {
        // check if were in a phase where we cannot select a character anymore
        if (GameManager.Instance._prepareCounselPhase || GameManager.Instance._prepareCounselUIOpened)
        {
            _characterPreview.enabled = false;
        }
        else if (GameManager.Instance._characterSelectionPhase)
        {
            _characterPreview.enabled = true;
        }
    }
}
