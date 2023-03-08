using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemy
{
    public class SpawnManger : MonoBehaviour
    {
        [field: SerializeField] 
        private Enemy enemy;
        
        [field: SerializeField] 
        private Transform[] spawnPoints;

        [field: SerializeField] 
        private AnimateFriction friction;
        
        [field: SerializeField] 
        private YellowDude yellowDude;

        private EnemyChest[] _liveEnemies;

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
                //make enemies spawn with delay todo
                yellowDude.ApplyTargets(dummy);
                _aliveEnemies++;
            }

            _liveEnemies = FindObjectsOfType<EnemyChest>();
            
            for (int i = 0; i < _liveEnemies.Length; i++)
            {
                EstablishEventConnections(i);
            }
            
            friction.SetCollidersFriction();
            
            _waveNumber++;
        }

        private void EstablishEventConnections(int i)
        {
            _liveEnemies[i].EnemyDeadEvent += DeadEnemiesCount;
        }

        private void DeadEnemiesCount(EventArgs _)
        {
            _aliveEnemies--;
            _score += 11;
        }
    }
}