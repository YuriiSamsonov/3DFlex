using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyHead : EnemyBodyPart
    {
        [field: SerializeField] 
        private GameObject[] allParts;

        private int _currentHp;
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };

        private Action _enemyDeadCallback;

        public void Init(Action enemyDeadCallback)
        {
            _enemyDeadCallback = enemyDeadCallback;
        }
        
        protected override void Awake()
        {
            base.Awake();
            _currentHp = partMaxHealth;
        }

        public override void OnHit(int damage)
        {
            _enemyDeadCallback();
            
            _currentHp = Mathf.Max(0, _currentHp - damage);
            
            var material = bpRenderer.material;
            material.color = Color.red;
            
            const float delay = 1.0f;
            StartCoroutine(ResetColorAfterDelay(delay, material));

            if (_currentHp <= 0)
            {
                bloodParent.SetActive(true);
                
                joints[0].connectedBody = null;
                joints[0].slerpDrive = _jointSpring;
                joints[0].yMotion = joints[0].zMotion = joints[0].yMotion = ConfigurableJointMotion.Free;

                if (enemyBodyParts != null)
                {
                    for (int i = 0; i < enemyBodyParts.Length; i++)
                    {
                        if (enemyBodyParts[i].TryGetComponent<PhysicalBodyPart>(out var bodyPart))
                        {
                            bodyPart.RemoveTarget();
                        }

                        if (enemyBodyParts[i].TryGetComponent<ConfigurableJoint>(out var joint))
                        {
                            joint.slerpDrive = _jointSpring;
                        }
                    }
                }

                for (int i = 0; i < allParts.Length; i++)
                {
                    const int criticalDamage = 10000;
                    if (allParts[i].TryGetComponent<EnemyChest>(out var chest))
                        chest.OnHit(criticalDamage);

                    StartCoroutine(DestroyTrash(i));

                    allParts[i].tag = Variables.UntaggedTag;
                }
            }
        }

        private IEnumerator DestroyTrash(int i)
        {
            yield return new WaitForSeconds(30);
            allParts[i].SetActive(false);
        }
    }
}