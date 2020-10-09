using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InitialMenuManager : MonoBehaviour
{
    [Header("Menu Configurations")] 
    private GameObject _initialMenu;
    private const string InitialMenuName = "InitialMenu";
    private const string PlayMenuName = "PlayMenu";
    private GameObject _playMenu;
    
    private MenuSoundButton _menuSoundButton;
    
    private void Start()
    {
        _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
        _initialMenu = MenuManager.GetMenuByName(InitialMenuName);
        _playMenu = MenuManager.GetMenuByName(PlayMenuName);
    }

    public void PlayButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        MenuManager.ChangeMenu(_initialMenu, _playMenu);
    }
}
