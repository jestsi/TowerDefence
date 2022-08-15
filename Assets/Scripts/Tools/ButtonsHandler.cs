using Assets.Scripts.Tools;
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
            SceneManager.LoadScene((int)ScenesIndexator.Menu);
            Debug.Log("Load main menu scene");
        }

        public void SetPause(bool isPause)
        {
            LevelHandler.IsPaused = isPause;
        }

        public void OnQuitButtonCliked()
        {
            Application.Quit();
        }
    }
}
