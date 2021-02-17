using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour
{
    public static MenuSceneManager Instance;

    [Header("Canvas Configurations")]
    public GameObject mainMenuCanvas;
    public GameObject fadeInImage;
    public GameObject fadeOutImage;
    private bool _fadingInImage;
    private float _fadingInImageTime = 1f;
    private bool _fadingOutImage;
    private float _fadingOutImageTime = 2f;
    
    [Header("Cameras")] 
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera prepareCounselRoomCamera;

    [Header("Animators")]
    public Animator theDoorsAnimator;
    private static readonly int StartDoorAnimation = Animator.StringToHash("StartDoorAnimation");

    [Header("Music")]
    public AudioSource mainMusic;
    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        EnterMenuScene();
    }

    private void Update()
    {
        if (_fadingInImage)
        {
            _fadingInImageTime -= Time.deltaTime;
            if (_fadingInImageTime <= 0)
            {
                fadeInImage.SetActive(false);
                _fadingInImage = false;
            }
        }
        
        if (_fadingOutImage)
        {
            _fadingOutImageTime -= Time.deltaTime;
            mainMusic.volume -= Time.deltaTime / 10;

            if (_fadingOutImageTime <= 0)
            {
                SceneManager.LoadScene("CharacterSelection");
                _fadingOutImage = false;
            }
        }
    }

    private void EnterMenuScene()
    {
        _fadingInImage = true;
        fadeInImage.SetActive(true);
    }

    public void EnterPrepareCouncilScene()
    {
        // start door animation
        theDoorsAnimator.SetBool(StartDoorAnimation, true);

        // dont show menu ui
        mainMenuCanvas.SetActive(false);
        
        // switch cameras to go towards door
        defaultCamera.enabled = false;
        prepareCounselRoomCamera.enabled = true;

        _fadingOutImage = true;
        fadeOutImage.SetActive(true);
    }
}
