using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class WaveNumberUI : MonoBehaviour
    {
        /// <summary>
        /// Enemy wave number text.
        /// </summary>
        [field: SerializeField, Tooltip("Enemy wave number text.")] 
        private Text waveNumberText;
        
        /// <summary>
        /// Enemy spawn manager.
        /// </summary>
        [field: SerializeField, Tooltip("Enemy spawn manager.")] 
        private SpawnManager spawnManager;

        /// <summary>
        /// Set new value to the wave count bar.
        /// </summary>
        public void UpdateWaveCount()
        {
            waveNumberText.text = "WAVE : " + spawnManager.WaveNumber;
        }
        
        
    }
}