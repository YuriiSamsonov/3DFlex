using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [field: SerializeField] 
        private SpawnManager spawnManager;
        
        [field: SerializeField] 
        private Text scoreText;

        public void UpdateScore()
        {
            scoreText.text = "Score : " + spawnManager.Score;
        }
    }
}