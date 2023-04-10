using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyHead : EnemyBodyPart
    {
        /// <summary>
        /// All body parts of the enemy
        /// </summary>
        [field: SerializeField, Tooltip("All body parts of the enemy")] 
        private GameObject[] allParts;

        private Action _enemyDeadCallback;

        public void Init(Action enemyDeadCallback)
        {
            _enemyDeadCallback = enemyDeadCallback;
        }

        public override void OnHit(int damage)
        {
            base.OnHit(damage);

            if (currentHp <= 0 && !isDead)
            {
                isDead = true;
                
                _enemyDeadCallback();
                
                const float delay = 30.0f;
    
                for (int i = 0; i < allParts.Length; i++)
                {
                    const int criticalDamage = 10000;
                    if (allParts[i].TryGetComponent<EnemyChest>(out var part))
                        part.OnHit(criticalDamage);

                    StartCoroutine(HideTrashWithDelay(i, delay));

                    allParts[i].tag = Variables.UntaggedTag;
                }
            }
        }

        /// <summary>
        /// Inactivate all enemy body parts
        /// </summary>
        private IEnumerator HideTrashWithDelay(int i, float delay)
        {
            yield return new WaitForSeconds(delay);
            allParts[i].SetActive(false);
        }
    }
}