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
        public AudioSource mainMusic;

        [Header("Cameras")] 
        public CinemachineVirtualCamera defaultCamera;
        public CinemachineVirtualCamera prepareCounselRoomCamera;
        
        private MenuManager _menuManager;
        private bool _switchingScene;

        public static CreateGameMenuManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _menuManager = gameObject.GetComponent<MenuManager>();
            
            var parent = transform.parent;
            _playMenu = _menuManager.GetMenuByName(parent.gameObject, PlayMenuName);
            _createGameMenu = _menuManager.GetMenuByName(parent.gameObject, CreateGameMenuName);
        }

        public void BackButton()
        {
            SoundManager.Instance.ButtonClickSound();
        
            _menuManager.ChangeMenu(_createGameMenu, _playMenu);
        }

        public void NumberOfPlayersDropdown(int dropdownSelected)
        {
            SoundManager.Instance.ButtonClickSound();
        }
        
        public void CreateGameButton()
        {
            SoundManager.Instance.ButtonClickSound();

            // start fade out of scene
            MenuSceneManager.Instance.EnterPrepareCounselScene();
        }
    }
}
