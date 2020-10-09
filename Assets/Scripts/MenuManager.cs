using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private static GameObject _parentCanvasToAllMenus;
    
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        _parentCanvasToAllMenus = transform.parent.gameObject;
    }

    public static GameObject GetMenuByName(string menuName)
    {
        for (var i = 0; i < _parentCanvasToAllMenus.transform.childCount; i++)
        {
            if (_parentCanvasToAllMenus.transform.GetChild(i).gameObject.name !=
                menuName) continue;
            
            return _parentCanvasToAllMenus.transform.GetChild(i).gameObject;
        }

        return null;
    }

    public static void ChangeMenu(GameObject menuToStopShowing, GameObject menuToShow)
    {
        menuToStopShowing.SetActive(false);
        menuToShow.SetActive(true);
    }
}
