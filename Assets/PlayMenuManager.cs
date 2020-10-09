using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuManager : MonoBehaviour
{
    [Header("Menu Configurations")] 
    private const string InitialMenuName = "InitialMenu";
    private const string PlayMenuName = "PlayMenu";
    private const string FindGameMenuName = "FindGameMenu";
    private const string CreateGameMenuName = "CreateGameMenu";
    private const string PrivateGameMenuName = "PrivateGameMenu";
    private GameObject _initialMenu;
    private GameObject _playMenu;
    private GameObject _findGameMenu;
    private GameObject _createGameMenu;
    private GameObject _privateGameMenu;
    
    private MenuSoundButton _menuSoundButton;
    
    private void Start()
    {
        _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
        _initialMenu = MenuManager.GetMenuByName(InitialMenuName);
        _playMenu = MenuManager.GetMenuByName(PlayMenuName);
        _findGameMenu = MenuManager.GetMenuByName(FindGameMenuName);
        _createGameMenu = MenuManager.GetMenuByName(CreateGameMenuName);
        _privateGameMenu = MenuManager.GetMenuByName(PrivateGameMenuName);
    }

    public void BackButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        MenuManager.ChangeMenu(_playMenu, _initialMenu);
    }
    
    public void FindGameButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        MenuManager.ChangeMenu(_playMenu, _findGameMenu);
    }
    
    public void CreateGameButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        MenuManager.ChangeMenu(_playMenu, _createGameMenu);
    }
    
    public void PrivateGameButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        MenuManager.ChangeMenu(_playMenu, _privateGameMenu);
    }
}
