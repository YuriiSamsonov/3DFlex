using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyChest : EnemyBodyPart
    {
        private int _currentHp; //replace to parent
        
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
        public event Event<EventArgs> EnemyDeadEvent;
        
        protected override void Awake()
        {
            base.Awake();
            _currentHp = partMaxHealth;
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
                for (int i = 0; i < joints.Length; i++)
                    if (joints[i].TryGetComponent(out ConfigurableJoint joint))
                        ReleaseJoints(joint);

                for (int i = 0; i < enemyBodyParts.Length; i++)
                {
                    if(enemyBodyParts[i].TryGetComponent<PhysicalBodyPart>(out var bodyPart))
                        bodyPart.RemoveTarget();
                    
                    if (enemyBodyParts[i].TryGetComponent<ConfigurableJoint>(out var joint))
                    {
                        joint.slerpDrive = _jointSpring;
                    }
                }
            }
        }
    }
}