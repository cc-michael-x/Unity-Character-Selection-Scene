using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialMenuManager : MonoBehaviour
{
    private MenuSoundButton _menuSoundButton;
    
    private void Start()
    {
        _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
    }

    public void PlayButton()
    {
        _menuSoundButton.PlayButtonClickSound();
    }
    
    public void SettingsButton()
    {
        _menuSoundButton.PlayButtonClickSound();
    }
}
