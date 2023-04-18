 using Game.Scripts.PlayerSpace;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    /// <summary>
    /// This class is used to control the behavior of the enemy arm body part.
    /// Inherit EnemyBodyPart.cs.
    /// </summary>
    public class EnemyArm : EnemyBodyPart
    {
        private PlayerMono _playerMono;
        
        /// <summary>
        /// Trigger collider that is used to trigger dealing damage to player.
        /// </summary>
        [field: SerializeField, Tooltip("Trigger collider that is used to trigger dealing damage to player.")]
        private BoxCollider damageCollider;

        protected override void Awake()
        {
            base.Awake();
            _playerMono = FindObjectOfType<PlayerMono>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Variables.PlayerTag))
                _playerMono.OnPlayerTakeDamage();
        }

        public override void OnHit(int damage)
        {
            base.OnHit(damage);
            
            if (currentHp <= 0)
                damageCollider.enabled = false;
        }
    }
}