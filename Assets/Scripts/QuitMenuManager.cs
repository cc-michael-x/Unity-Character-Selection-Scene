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

    
    private MenuManager _menuManager;
    
    private void Start()
    {
        _menuManager = gameObject.GetComponent<MenuManager>();
        
        var parent = transform.parent;
        _initialMenu = _menuManager.GetMenuByName(parent.gameObject, InitialMenuName);
        _quitMenu = _menuManager.GetMenuByName(parent.gameObject, QuitMenuName);
    }

    public void NoButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        _menuManager.ChangeMenu(_quitMenu, _initialMenu);
    }
    
    public void YesButton()
    {
        SoundManager.Instance.ButtonClickSound();
        
        Application.Quit();
    }
}
