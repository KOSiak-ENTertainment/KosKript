using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenuScripts
{
    public class PauseMenuScript : MonoBehaviour
    {
        public GameObject pauseMenu;
        [SerializeField] private KeyCode pauseMenuKey;
        private bool _isGamePaused;
        
        public void ContinueGame()
        {
            _isGamePaused = false;
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        private void Start()
        {
            pauseMenu.SetActive(false);
        }

        private void Update()
        {
            ActiveMenu();
        }

        private void ActiveMenu()
        {
            if (Input.GetKeyDown(pauseMenuKey))
                _isGamePaused = !_isGamePaused;

            pauseMenu.SetActive(_isGamePaused);
            Time.timeScale = !_isGamePaused 
                ? 0f 
                : 1f;
        }
    }
}
