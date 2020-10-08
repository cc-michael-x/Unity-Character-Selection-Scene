using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreviewSelectButtonHandler : MonoBehaviour
{
    public GameObject parentCanvas;
    
    public void OnCharacterPreviewSelectButton()
    {
        Debug.Log(parentCanvas.transform.parent.gameObject.name);
    }
}
