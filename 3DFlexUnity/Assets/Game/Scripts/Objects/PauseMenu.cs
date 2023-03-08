using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Objects
{
    public class PauseMenu : MonoBehaviour
    {
        public void OnExitButton()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }
    }
}