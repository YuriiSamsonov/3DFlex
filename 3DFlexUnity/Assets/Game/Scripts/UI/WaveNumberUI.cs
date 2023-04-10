using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class WaveNumberUI : MonoBehaviour
    {
        [field: SerializeField] 
        private Text scoreAndWaveText;
        
        [field: SerializeField] 
        private SpawnManager spawnManager;

        public void UpdateWaveCount()
        {
            scoreAndWaveText.text = "WAVE : " + spawnManager.WaveNumber;
        }
        
        
    }
}