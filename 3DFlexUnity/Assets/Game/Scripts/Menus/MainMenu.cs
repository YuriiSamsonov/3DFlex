using System;
using System.Linq;
using Game.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Game.Scripts.Objects
{
    public class MainMenu : MonoBehaviour
    {
        
        [field: SerializeField] 
        private GameObject cup;

        [field: SerializeField] 
        private Texture2D[] cupTexture;

        private Renderer _cupRenderer;
        
        [field: SerializeField]
        private CupRuntimeData cupRuntimeData;
        
        private int _currentTexture;

        private void Start()
        {
            cupRuntimeData.cupTexture = cupTexture[0];
            _cupRenderer = cup.GetComponent<Renderer>();
            ApplyTexture();

            Time.timeScale = 1f;
        }

        private void FixedUpdate()
        {
            if (cup != null)
            {
                cup.transform.Rotate(0,1,0, Space.Self);
            }
        }
        
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void RightCupTexture()
        {
            if (_currentTexture >= cupTexture.Length - 1)
            {
                _currentTexture = -1;
            }
            
            _currentTexture++;
            ApplyTexture();
        }

        public void LeftCupTexture()
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