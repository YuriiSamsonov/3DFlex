using System;
using Game.Scripts.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [field: SerializeField] 
        private SpawnManger spawnManger;
        
        [field: SerializeField] 
        private Text scoreText;

        private void Update()
        {
            scoreText.text = "Score : " + spawnManger.Score;
        }
    }
}