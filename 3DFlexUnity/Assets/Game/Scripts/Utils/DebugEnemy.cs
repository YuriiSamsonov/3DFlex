using System;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class DebugEnemy : MonoBehaviour
    {
        
        private EnemyHead[] _liveEnemies;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _liveEnemies = FindObjectsOfType<EnemyHead>();
                for (int i = 0; i < _liveEnemies.Length; i++) 
                {
                    _liveEnemies[i].OnHit(10000);
                }

            }
        }
    }
}