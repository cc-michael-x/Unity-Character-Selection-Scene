using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using UnityEngine;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public GameObject[] _characters;

    private void Start()
    {
        Instance = this;
    }

    public void SetCurrentCharacterPreview(string characterName)
    {
        GameObject[] nonSelectedCharacterPreviews = 
            _characters.Where(character => character.name != characterName).ToArray();

        foreach (var nonSelectedCharacterPreview in nonSelectedCharacterPreviews)
        {
            nonSelectedCharacterPreview.GetComponent<CharacterPreview>().enabled = characterName.IsNullOrEmpty();
        }
    }
} 
