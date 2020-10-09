using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    private const string CharacterPreviewComponents = "CharacterPreviewComponents";
    public static GameManager Instance;

    public string currentCharacterSelected;
    public GameObject characters;
    public GameObject[] charactersArray;

    public Texture2D defaultCursor;

    public GameObject moveCameraMouseTipCanvas;
    
    private void Start()
    {
        Instance = this;

        // set default cursor
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        
        // dynamically fetch character list from parent character game object
        charactersArray = new GameObject[characters.transform.childCount];
        for (var i = 0; i < characters.transform.childCount; i++)
        {
            for (var x = 0; x < characters.transform.GetChild(i).transform.childCount; x++)
            {
                if (characters.transform.GetChild(i).GetChild(x).gameObject.name !=
                    CharacterPreviewComponents) continue;
                charactersArray[i] = characters.transform.GetChild(i).GetChild(x).gameObject;
                x = characters.transform.GetChild(i).transform.childCount;
            }
        }
        
        // confine the cursor to the screen
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        // stop showing move camera tooltip
        if(Input.GetMouseButton(1))
            moveCameraMouseTipCanvas.SetActive(false);

        // show the cursor when the user releases the right mouse button
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    // disable all other character preview components that isn't the currently selected character preview
    public void SetCurrentCharacterPreview(string characterName)
    {
        if (!characterName.IsNullOrEmpty())
            currentCharacterSelected = characterName;
        
        GameObject[] nonSelectedCharacterPreviews = 
            charactersArray.Where(character => 
                character.GetComponent<CharacterPreview>().characterName != characterName).ToArray();

        foreach (var nonSelectedCharacterPreview in nonSelectedCharacterPreviews)
        {
            nonSelectedCharacterPreview.GetComponent<CharacterPreview>().enabled = characterName.IsNullOrEmpty();
        }
    }
} 
