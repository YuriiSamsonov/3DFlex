using System;
using System.Collections;
using Game.Scripts.PlayerSpace;
using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyArm : EnemyBodyPart
    {
        private int _currentHp;
        private PlayerMono _playerMono;

        protected override void Awake()
        {
            base.Awake();
            _currentHp = partMaxHealth;
            _playerMono = GetComponent<PlayerMono>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                _playerMono.OnHit();
        }

        public override void OnHit(int damage)
        {
            _currentHp = Mathf.Max(0, _currentHp - damage);
            
            var material = bpRenderer.material;
            material.color = Color.red;

            const float delay = 1.0f;
            StartCoroutine(ResetColorAfterDelay(delay, material));

            if (_currentHp <= 0)
            {
                joints[0].GetComponent<PhysicalBodyPart>().enabled = false;
                Destroy(joints[0].GetComponent<ConfigurableJoint>());
                bloodParent.SetActive(true);
            }
        }
    }
}