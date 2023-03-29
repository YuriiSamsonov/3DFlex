using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        [field: SerializeField] 
        private Enemy enemy;
        
        [field: SerializeField] 
        private Transform[] spawnPoints;

        [field: SerializeField] 
        private AnimateFriction friction;
        
        [field: SerializeField] 
        private YellowDude yellowDude;

        private EnemyHead[] _liveEnemies;

        private int _randomPos;
        private int _waveNumber;
        public int WaveNumber => _waveNumber;
        
        private int _aliveEnemies;
        
        public int AliveEnemies => _aliveEnemies;

        private int _score;
        public int Score => _score;

        private void FixedUpdate()
        {
            if (_aliveEnemies <= 0)
            {
                SpawnNewWave();
            }
        }
        
        private void SpawnNewWave()
        {
            for (int i = 0; i < _waveNumber + 1; i++)
            {
                var dummy = Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)]);
                yellowDude.ApplyTargets(dummy);
                _aliveEnemies++;
            }

            _liveEnemies = FindObjectsOfType<EnemyHead>();
            
            for (int i = 0; i < _liveEnemies.Length; i++)
            {
                _liveEnemies[i].Init(DeadEnemiesCount);
            }
            
            friction.SetCollidersFriction();
            
            _waveNumber++;
        }

        private void DeadEnemiesCount()
        {
            _aliveEnemies--;
            _score += 11;
        }
    }
}