using Game.Scripts.ScriptableObjects;
using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Menus
{
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Cup to rotate in scene.
        /// </summary>
        [field: SerializeField] 
        private GameObject cup;
        
        [field: SerializeField] 
        private float rotationSpeed = 0.02f;

        [field: SerializeField] 
        private Texture2D[] cupTexture;

        private Renderer _cupRenderer;
        
        [field: SerializeField]
        private CupRuntimeData cupRuntimeData;
        
        private int _currentTexture;
        private float _rotation;

        private void Start()
        {
            cupRuntimeData.cupTexture = cupTexture[0];
            _cupRenderer = cup.GetComponent<Renderer>();
            ApplyTexture();
            _rotation = rotationSpeed / Time.fixedDeltaTime; // difficult..............

            Time.timeScale = 1f;
        }

        private void FixedUpdate()
        {
            cup.transform.Rotate(0,_rotation,0, Space.Self);
        }
        
        public void PlayGame()
        {
            SceneManager.LoadScene(Variables.MainSceneBuildIndex);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void OnButtonNextTexture()
        {
            if (_currentTexture >= cupTexture.Length - 1)
            {
                _currentTexture = -1;
            }
            
            _currentTexture++;
            ApplyTexture();
        }

        public void OnButtonPreviousTexture()
        {
            if (_currentTexture <= 0)
            {
                _currentTexture = cupTexture.Length;
            }
            
            _currentTexture--;
            ApplyTexture();
        }

        private void ApplyTexture()
        {
            _cupRenderer.material.mainTexture = cupTexture[_currentTexture];
            cupRuntimeData.cupTexture = cupTexture[_currentTexture];
        }
    }
}