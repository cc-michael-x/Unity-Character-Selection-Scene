using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    
    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;
    }

    public GameObject GetMenuByName(GameObject parentCanvasToAllMenus, string menuName)
    {
        for (var i = 0; i < parentCanvasToAllMenus.transform.childCount; i++)
        {
            if (parentCanvasToAllMenus.transform.GetChild(i).gameObject.name !=
                menuName) continue;
            
            return parentCanvasToAllMenus.transform.GetChild(i).gameObject;
        }

        return null;
    }

    public void ChangeMenu(GameObject menuToStopShowing, GameObject menuToShow)
    {
        menuToStopShowing.SetActive(false);
        menuToShow.SetActive(true);
    }
}
