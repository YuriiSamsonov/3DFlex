using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// Load MainMenuScene and set normal time scale.
        /// </summary>
        public void OnPauseMenuExitButton()
        {
            SceneManager.LoadScene(Variables.MainMenuSceneBuildIndex);
            Time.timeScale = 1f;
        }
    }
}