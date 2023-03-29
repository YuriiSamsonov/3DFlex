using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [FormerlySerializedAs("spawnManger")] [field: SerializeField] 
        private SpawnManager spawnManager;
        
        [field: SerializeField] 
        private Text scoreText;

        private void Update()
        {
            scoreText.text = "Score : " + spawnManager.Score;
        }
    }
}