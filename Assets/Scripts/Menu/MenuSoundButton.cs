using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundButton : MonoBehaviour
{
    private AudioSource _buttonSound;
    
    // Start is called before the first frame update
    void Start()
    {
        _buttonSound = GameObject.Find("ButtonSound").gameObject.GetComponent<AudioSource>();
    }

    public void PlayButtonClickSound()
    {
        _buttonSound.Play();
    }
}
