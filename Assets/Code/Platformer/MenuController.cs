

using UnityEngine;
using UnityEngine.SceneManagement;
namespace Platformer
{
    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;

      
        public GameObject mainMenu;
        public GameObject optionsMenu;
        public GameObject levelMenu;

        void Awake()
        {
            instance = this;
            Hide(); 
        }

        void SwitchMenu(GameObject someMenu)
        {
            
            mainMenu.SetActive(false);
            optionsMenu.SetActive(false);
            levelMenu.SetActive(false);

            
            someMenu.SetActive(true);
        }

        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }

        public void ShowOptionsMenu()
        {
            SwitchMenu(optionsMenu);
        }

        public void ShowLevelMenu()
        {
            SwitchMenu(levelMenu);
        }

        public void Show()
        {
            ShowMainMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayerController.instance.isPaused = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            if (PlayerController.instance != null)
            {
                PlayerController.instance.isPaused = false;
            }
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}