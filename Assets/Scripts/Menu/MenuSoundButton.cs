using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundButton : MonoBehaviour
{
    public AudioSource buttonSound;
    
    public void PlayButtonClickSound()
    {
        buttonSound.Play();
    }
}
