using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts.Tools
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
