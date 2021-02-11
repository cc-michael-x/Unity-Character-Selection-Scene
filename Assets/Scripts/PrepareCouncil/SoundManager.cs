using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource buttonClickSound;

    private void Start()
    {
        Instance = this;
    }

    public void ButtonClickSound()
    {
        buttonClickSound.Play();
    }
}
