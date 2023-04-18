using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    /// <summary>
    /// This class is used to control the behavior of the enemy leg body part.
    /// Inherit EnemyBodyPart.cs.
    /// </summary>
    public class EnemyLeg : EnemyBodyPart
    {
        /// <summary>
        /// Feet collider. Is used to change friction and to move the enemy forward.
        /// </summary>
        [field: SerializeField, Tooltip("Feet collider. Is used to change friction and to move the enemy forward.")] 
        private BoxCollider colToChangeMaterial;
        
        /// <summary>
        /// Feet collider.
        /// Uses for change friction and moving enemy forward. 
        /// </summary>
        public BoxCollider ColToChangeMaterial => colToChangeMaterial;

        public override void OnHit(int damage)
        {
            base.OnHit(damage);

            if (currentHp <= 0)
                ReleaseMainJoint();
        }
    }
}