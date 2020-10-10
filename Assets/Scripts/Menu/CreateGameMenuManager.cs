using System;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class CreateGameMenuManager : MonoBehaviour
    {
        [Header("Menu Configurations")] 
        private const string PlayMenuName = "PlayMenu";
        private const string CreateGameMenuName = "CreateGameMenu";
        private GameObject _playMenu;
        private GameObject _createGameMenu;

        [Header("Sounds")]
        private MenuSoundButton _menuSoundButton;

        [Header("Cameras")] 
        public CinemachineVirtualCamera defaultCamera;
        public CinemachineVirtualCamera prepareCounselRoomCamera;

        public GameObject fadeOut;
        public GameObject theDoors;
        private Animator _theDoorsAnimator;

        private MenuManager _menuManager;
        private bool _switchingScene;
        private float _sceneSwitchTime = 3f;
        private void Start()
        {
            _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
            
            _menuManager = gameObject.GetComponent<MenuManager>();
            
            var parent = transform.parent;
            _playMenu = _menuManager.GetMenuByName(parent.gameObject, PlayMenuName);
            _createGameMenu = _menuManager.GetMenuByName(parent.gameObject, CreateGameMenuName);

            _theDoorsAnimator = theDoors.GetComponent<Animator>();
        }

        private void Update()
        {
            if (_switchingScene)
            {
                _sceneSwitchTime -= Time.deltaTime;
                if(_sceneSwitchTime <= 0) 
                    SceneManager.LoadScene("PrepareCounselMeeting");
            }
        }

        public void BackButton()
        {
            _menuSoundButton.PlayButtonClickSound();
        
            _menuManager.ChangeMenu(_createGameMenu, _playMenu);
        }

        public void NumberOfPlayersDropdown(int dropdownSelected)
        {
            _menuSoundButton.PlayButtonClickSound();
        }
        
        public void CreateGameButton()
        {
            _menuSoundButton.PlayButtonClickSound();
            
            _theDoorsAnimator.SetBool("StartDoorAnimation", true);

            fadeOut.SetActive(true);
            //transform.root.gameObject.SetActive(false);
            defaultCamera.enabled = false;
            prepareCounselRoomCamera.enabled = true;

            _switchingScene = true;
        }
    }
}
