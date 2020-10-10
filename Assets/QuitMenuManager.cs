using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuManager : MonoBehaviour
{
    [Header("Menu Configurations")] 
    private const string InitialMenuName = "InitialMenu";
    private const string QuitMenuName = "QuitMenu";
    private GameObject _initialMenu;
    private GameObject _quitMenu;

    private MenuSoundButton _menuSoundButton;
    
    private MenuManager _menuManager;
    
    private void Start()
    {
        _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
        _menuManager = gameObject.GetComponent<MenuManager>();
        
        var parent = transform.parent;
        _initialMenu = _menuManager.GetMenuByName(parent.gameObject, InitialMenuName);
        _quitMenu = _menuManager.GetMenuByName(parent.gameObject, QuitMenuName);
    }

    public void NoButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        _menuManager.ChangeMenu(_quitMenu, _initialMenu);
    }
    
    public void YesButton()
    {
        _menuSoundButton.PlayButtonClickSound();
        
        Application.Quit();
    }
}
