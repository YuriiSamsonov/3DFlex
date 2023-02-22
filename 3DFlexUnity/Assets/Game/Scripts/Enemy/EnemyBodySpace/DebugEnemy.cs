using System;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class DebugEnemy : MonoBehaviour
    {
        [field: SerializeField] 
        private EnemyChest part;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                part.OnHit(100);
            }
        }
    }
}