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
    
    private MenuManager _menuManager;
    
    private void Start()
    {
        _menuManager = gameObject.GetComponent<MenuManager>();
        
        var parent = transform.parent;
        _initialMenu = _menuManager.GetMenuByName(parent.gameObject, InitialMenuName);
        _playMenu = _menuManager.GetMenuByName(parent.gameObject, PlayMenuName);
        _findGameMenu = _menuManager.GetMenuByName(parent.gameObject, FindGameMenuName);
        _createGameMenu = _menuManager.GetMenuByName(parent.gameObject, CreateGameMenuName);
        _privateGameMenu = _menuManager.GetMenuByName(parent.gameObject, PrivateGameMenuName);
    }

    public void BackButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        _menuManager.ChangeMenu(_playMenu, _initialMenu);
    }
    
    public void FindGameButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        _menuManager.ChangeMenu(_playMenu, _findGameMenu);
    }
    
    public void CreateGameButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        _menuManager.ChangeMenu(_playMenu, _createGameMenu);
    }
    
    public void PrivateGameButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        _menuManager.ChangeMenu(_playMenu, _privateGameMenu);
    }
}
