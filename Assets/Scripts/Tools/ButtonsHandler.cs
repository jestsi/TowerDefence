using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class ButtonsHandler : MonoBehaviour
    {
        public void OnPlayButtonClicked()
        {
            SceneManager.LoadScene((int)ScenesIndexator.Main);
            Debug.Log("Load main scene");
        }

        public void BackToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene((int)ScenesIndexator.Menu);
        }

        public void OnButtonPauseClicked()
        {
            LevelHandler.IsPaused = !LevelHandler.IsPaused;
            Time.timeScale = LevelHandler.IsPaused ? 0 : 1;
        }

        public void OnQuitButtonCliked()
        {
            Application.Quit();
        }
    }
}