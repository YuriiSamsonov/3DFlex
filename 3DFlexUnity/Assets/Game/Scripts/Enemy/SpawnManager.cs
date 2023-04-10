using System;
using Game.Scripts.Enemy.EnemyBodySpace;
using Game.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        /// <summary>
        /// Prefab of the enemy to spawn in the scene.
        /// </summary>
        [field: SerializeField, Tooltip("Prefab of the enemy to spawn in the scene.")] 
        private Enemy enemyPrefab;
        
        /// <summary>
        /// Positions of possible enemy spawns.
        /// </summary>
        [field: SerializeField, Tooltip("Positions of possible enemy spawns.")] 
        private Transform[] spawnPoints;

        /// <summary>
        /// Animate friction Class.
        /// </summary>
        [field: SerializeField, Tooltip("Animate friction Class.")] 
        private AnimateFriction friction;
        
        /// <summary>
        /// Reference body for copying animations.
        /// </summary>
        [field: SerializeField, Tooltip("Reference body for copying animations.")] 
        private YellowDude yellowDude;

        private EnemyHead[] _liveEnemies;
        
        private int _waveNumber;
        /// <summary>
        /// Number of the enemy wave.
        /// The higher number of wave the more enemy spawns.
        /// </summary>
        public int WaveNumber => _waveNumber;

        private int _score;
        /// <summary>
        /// 
        /// </summary>
        public int Score => _score;
        
        private int _aliveEnemies;

        /// <summary>
        /// 
        /// </summary>
        public event Event<EventArgs> OnSpawnNewWave;

        private void Start()
        {
            SpawnNewWave();
        }

        /// <summary>
        /// Spawn enemies in random spawn points.
        /// Amount of enemies depends on wave number.
        /// </summary>
        private void SpawnNewWave()
        {
            for (int i = 0; i < _waveNumber + 1; i++)
            {
                var enemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)]);
                enemy.EnemyHead.Init(EnemyDiedCallback);
                friction.AddNewLegs(enemy.LeftLeg, enemy.RightLeg);
                yellowDude.ApplyTargets(enemy);
                
                _aliveEnemies++;
            }
              
            _waveNumber++;
            OnSpawnNewWave(EventArgs.Empty);
        }

        /// <summary>
        /// On enemy death, updates the score and makes a check to spawn new wave
        /// </summary>
        private void EnemyDiedCallback()
        {
            _aliveEnemies--;
            _score += 11;
            CheckNewWaveSpawnAvailability();
        }

        /// <summary>
        /// If amount of alive enemies less then one - spawn new wave of enemies.
        /// </summary>
        private void CheckNewWaveSpawnAvailability()
        {
            if (_aliveEnemies <= 0)
                SpawnNewWave();
        }
    }
}