using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    /// <summary>
    /// This class is used to control the behavior of the enemy head body part.
    /// Inherit EnemyBodyPart.cs.
    /// </summary>
    public class EnemyHead : EnemyBodyPart
    {
        /// <summary>
        /// All body parts of the enemy
        /// </summary>
        [field: SerializeField, Tooltip("All body parts of the enemy")] 
        private GameObject[] allParts;

        private Action _enemyDeadCallback;

        /// <summary>
        /// Establish enemyDeadCallback connection.
        /// </summary>
        /// <param name="enemyDeadCallback"></param>
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
                    var part = allParts[i];
                    if (part.TryGetComponent<EnemyChest>(out var enemyBodyPart))
                        enemyBodyPart.OnHit(criticalDamage);

                    StartCoroutine(HideBodyPartWithDelay(part, delay));

                    part.tag = Variables.UntaggedTag;
                }
            }
        }
        
        private IEnumerator HideBodyPartWithDelay(GameObject part, float delay)
        {
            yield return new WaitForSeconds(delay);
            part.SetActive(false);
        }
    }
}