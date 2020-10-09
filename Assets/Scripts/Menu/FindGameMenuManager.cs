using UnityEngine;

namespace Menu
{
    public class FindGameMenuManager : MonoBehaviour
    {
        [Header("Menu Configurations")] 
        private const string PlayMenuName = "PlayMenu";
        private const string FindGameMenuName = "FindGameMenu";
        private GameObject _playMenu;
        private GameObject _findGameMenu;

        private MenuSoundButton _menuSoundButton;
    
        private void Start()
        {
            _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
            _playMenu = MenuManager.GetMenuByName(PlayMenuName);
            _findGameMenu = MenuManager.GetMenuByName(FindGameMenuName);
        }

        public void BackButton()
        {
            _menuSoundButton.PlayButtonClickSound();
        
            MenuManager.ChangeMenu(_findGameMenu, _playMenu);
        }
    }
}
