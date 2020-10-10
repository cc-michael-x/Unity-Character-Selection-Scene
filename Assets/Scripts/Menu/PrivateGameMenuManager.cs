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
        private MenuManager _menuManager;
        
        private void Start()
        {
            _menuManager = gameObject.GetComponent<MenuManager>();
            
            var parent = transform.parent;
            _playMenu = _menuManager.GetMenuByName(parent.gameObject, PlayMenuName);
            _privateGameMenu = _menuManager.GetMenuByName(parent.gameObject, PrivateGameMenuName);
        }

        public void BackButton()
        {
            SoundManager.Instance.ButtonClickSound();
            
            _menuManager.ChangeMenu(_privateGameMenu, _playMenu);
        }
    }
}
