using UnityEngine;

namespace Menu
{
    public class PrivateGameMenuManager : MonoBehaviour
    {
        [Header("Menu Configurations")] 
        private const string PlayMenuName = "PlayMenu";
        private const string PrivateGameMenuName = "PrivateGameMenu";
        private GameObject _playMenu;
        private GameObject _privateGameMenu;

        private MenuSoundButton _menuSoundButton;
    
        private void Start()
        {
            _menuSoundButton = gameObject.GetComponent<MenuSoundButton>();
        
            _playMenu = MenuManager.GetMenuByName(PlayMenuName);
            _privateGameMenu = MenuManager.GetMenuByName(PrivateGameMenuName);
        }

        public void BackButton()
        {
            _menuSoundButton.PlayButtonClickSound();
        
            MenuManager.ChangeMenu(_privateGameMenu, _playMenu);
        }
    }
}
