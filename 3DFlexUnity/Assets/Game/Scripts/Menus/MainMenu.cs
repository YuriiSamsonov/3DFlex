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
        /// Amount of times the object rotates around itself per minute.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Amount of times the object rotates around itself per minute.")] 
        private float rotationsPerMinute = 300;

        /// <summary>
        /// All cup textures available for choosing.
        /// </summary>
        [field: SerializeField, Tooltip("All cup textures available for choosing.")] 
        private Texture2D[] cupTexture;
        
        /// <summary>
        /// Data to save desired cup texture.
        /// </summary>
        [field: SerializeField, Tooltip("Data to save desired cup texture.")]
        private CupRuntimeData cupRuntimeData;

        private Renderer _cupRenderer;
        
        private int _currentTexture;
        private float _rotationsPerTick;

        private void Start()
        {
            cupRuntimeData.cupTexture = cupTexture[0];
            _cupRenderer = cup.GetComponent<Renderer>();
            ApplyTexture();

            //Calculate the total amount in degrees the cup will rotate per tick, to use later.
            var rotationsPerSecond = rotationsPerMinute / 60;
            var degreesPerSecond = 360 * rotationsPerSecond;
            _rotationsPerTick = degreesPerSecond / (1 / Time.fixedDeltaTime);

            Time.timeScale = 1f;
        }

        private void FixedUpdate()
        {
            //cup rotating
            cup.transform.Rotate(0,_rotationsPerTick,0, Space.Self);
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
                _currentTexture = -1;

            _currentTexture++;
            ApplyTexture();
        }

        /// <summary>
        /// Select previous texture and save it in runtime data.
        /// </summary>
        public void OnButtonPreviousTexture()
        {
            if (_currentTexture <= 0)
                _currentTexture = cupTexture.Length;

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