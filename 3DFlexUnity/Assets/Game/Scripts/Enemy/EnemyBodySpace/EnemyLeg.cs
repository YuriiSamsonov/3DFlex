using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyLeg : EnemyBodyPart
    {
        [field: SerializeField, Tooltip("Feet collider. Uses for change friction and moving enemy forward.")] 
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
            {
                ReleaseMainJoint();
            }
        }
    }
}