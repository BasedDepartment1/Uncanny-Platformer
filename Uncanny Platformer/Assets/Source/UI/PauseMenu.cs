using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private Canvas hpCanvas;
        private static bool isPaused = false;
        public GameObject pauseMenuUI;
        
        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

        void Pause()
        {
            hpCanvas.enabled = false;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void Resume()
        {
            hpCanvas.enabled = true;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartLevelAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Resume();
        }
    }
}
