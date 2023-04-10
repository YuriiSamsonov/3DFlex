using Game.Scripts.PlayerSpace;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyArm : EnemyBodyPart
    {
        private PlayerMono _playerMono;
        
        /// <summary>
        /// Trigger collider which trigger PlayerMono.OnPlayerTakeDamage()
        /// </summary>
        [field: SerializeField, Tooltip("Collider which trigger PlayerMono.OnPlayerTakeDamage()")]
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