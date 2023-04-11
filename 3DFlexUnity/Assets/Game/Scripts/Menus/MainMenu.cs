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
        [field: SerializeField, Tooltip("Cup to rotate in scene.")] 
        private GameObject cup;
        
        /// <summary>
        /// ??????????
        /// </summary>
        [field: SerializeField, Tooltip("")] 
        private float rotationSpeed = 0.02f;

        /// <summary>
        /// Array of cup textures.
        /// </summary>
        [field: SerializeField, Tooltip("Array of cup textures.")] 
        private Texture2D[] cupTexture;
        
        /// <summary>
        /// Data to save desired cup texture.
        /// </summary>
        [field: SerializeField, Tooltip("Data to save desired cup texture.")]
        private CupRuntimeData cupRuntimeData;

        private Renderer _cupRenderer;
        
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
            //cup rotating
            cup.transform.Rotate(0,_rotation,0, Space.Self);
        }

        /// <summary>
        /// Load MainScene.
        /// </summary>
        public void OnButtonPlay()
        {
            SceneManager.LoadScene(Variables.MainSceneBuildIndex);
        }

        /// <summary>
        /// Quit application.
        /// </summary>
        public void OnButtonExit()
        {
            Application.Quit();
        }

        /// <summary>
        /// Select next texture and save it in runtime data.
        /// </summary>
        public void OnButtonNextTexture()
        {
            if (_currentTexture >= cupTexture.Length - 1)
            {
                _currentTexture = -1;
            }
            
            _currentTexture++;
            ApplyTexture();
        }

        /// <summary>
        /// Select previous texture and save it in runtime data.
        /// </summary>
        public void OnButtonPreviousTexture()
        {
            if (_currentTexture <= 0)
            {
                _currentTexture = cupTexture.Length;
            }
            
            _currentTexture--;
            ApplyTexture();
        }

        /// <summary>
        /// Apply selected texture and save it in runtime data.
        /// </summary>
        private void ApplyTexture()
        {
            _cupRenderer.material.mainTexture = cupTexture[_currentTexture];
            cupRuntimeData.cupTexture = cupTexture[_currentTexture];
        }
    }
}