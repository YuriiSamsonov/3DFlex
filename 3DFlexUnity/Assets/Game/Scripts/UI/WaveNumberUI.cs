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
        private Text scoreText;
        
        [FormerlySerializedAs("spawnManger")] [field: SerializeField] 
        private SpawnManager spawnManager;

        private void Update()
        {
            scoreText.text = "WAVE : " + spawnManager.WaveNumber;
        }
        
        
    }
}