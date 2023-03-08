using System;
using System.Linq;
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
        private GameObject block;
        
        [field: SerializeField] 
        private Texture2D[] cupTexture;

        public Texture2D[] CupTexture => cupTexture;

        private Renderer _cupRenderer;
        
        public static int CurrentTexture;
        public static int MaxPoints;

        private void Start()
        {
            if (cup != null)
            {
                _cupRenderer = cup.GetComponent<Renderer>();
                _cupRenderer.material.mainTexture = cupTexture[CurrentTexture];
            }

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
            if (CurrentTexture >= cupTexture.Length - 1)
            {
                CurrentTexture = -1;
            }
            
            CurrentTexture++;
            _cupRenderer.material.mainTexture = cupTexture[CurrentTexture];
        }

        public void LeftCupTexture()
        {
            if (CurrentTexture <= 0)
            {
                CurrentTexture = cupTexture.Length;
            }
            
            CurrentTexture--;
            _cupRenderer.material.mainTexture = cupTexture[CurrentTexture];
        }
    }
}