using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        /// <summary>
        /// Enemy spawn manager.
        /// </summary>
        [field: SerializeField, Tooltip("Enemy spawn manager.")] 
        private SpawnManager spawnManager;
        
        /// <summary>
        /// Game score text.
        /// </summary>
        [field: SerializeField, Tooltip("Game score text.")] 
        private Text scoreText;

        /// <summary>
        /// Set new value to the score bar.
        /// </summary>
        public void UpdateScore()
        {
            scoreText.text = "Score : " + spawnManager.Score;
        }
    }
}