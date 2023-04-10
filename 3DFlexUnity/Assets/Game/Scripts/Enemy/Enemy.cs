using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField, Tooltip("All PhysicalBodyParts of the enemy.")] 
        private PhysicalBodyPart[] bodyParts;
        /// <summary>
        /// All PhysicalBodyParts of the enemy.
        /// </summary>
        public PhysicalBodyPart[] BodyParts => bodyParts;
        
        [field: SerializeField, Tooltip("Head of the enemy.")] 
        private EnemyHead enemyHead;
        /// <summary>
        /// Head of the enemy.
        /// </summary>
        public EnemyHead EnemyHead => enemyHead;
        
        [field: SerializeField, Tooltip("Left leg of the enemy.")] 
        private EnemyLeg leftLeg;
        /// <summary>
        /// Left leg of the enemy.
        /// </summary>
        public EnemyLeg LeftLeg => leftLeg;
        
        [field: SerializeField, Tooltip("Right leg of the enemy.")] 
        private EnemyLeg rightLeg;
        /// <summary>
        /// Right leg of the enemy.
        /// </summary>
        public EnemyLeg RightLeg => rightLeg;
    }
}