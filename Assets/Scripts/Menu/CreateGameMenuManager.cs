using UnityEngine;

namespace Menu
{
    public class CreateGameMenuManager : MonoBehaviour
    {
        [Header("Menu Configurations")] 
        private const string PlayMenuName = "PlayMenu";
        private const string CreateGameMenuName = "CreateGameMenu";
        private GameObject _playMenu;
        private GameObject _createGameMenu;

        private MenuSoundButton _menuSoundButton;
    
        private void Start()
        {
            _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
            _playMenu = MenuManager.GetMenuByName(PlayMenuName);
            _createGameMenu = MenuManager.GetMenuByName(CreateGameMenuName);
        }

        public void BackButton()
        {
            _menuSoundButton.PlayButtonClickSound();
        
            MenuManager.ChangeMenu(_createGameMenu, _playMenu);
        }
    }
}
