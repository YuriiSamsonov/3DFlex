using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyChest : EnemyBodyPart
    {
        /// <summary>
        /// Head of the enemy
        /// </summary>
        [field: SerializeField, Tooltip("Head of the enemy")] 
        private EnemyHead head;
        
        /// <summary>
        /// Arms of the enemy
        /// </summary>
        [field: SerializeField, Tooltip("Arms of the enemy")] 
        private EnemyArm[] arms;
        public override void OnHit(int damage)
        {
            base.OnHit(damage);

            if (currentHp <= 0 && !isDead)
            {
                isDead = true;
                const int criticalDamage = 1000;
                head.OnHit(criticalDamage);
                
                for (int i = 0; i < arms.Length; i++)
                {
                    arms[i].OnHit(criticalDamage);
                }
            }
        }
    }
}