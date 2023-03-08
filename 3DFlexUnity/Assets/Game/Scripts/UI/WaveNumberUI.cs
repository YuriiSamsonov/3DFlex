using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class WaveNumberUI : MonoBehaviour
    {
        [field: SerializeField] 
        private Text scoreText;
        
        [field: SerializeField] 
        private SpawnManger spawnManger;

        private void Update()
        {
            scoreText.text = "WAVE : " + spawnManger.WaveNumber;
        }
        
        
    }
}