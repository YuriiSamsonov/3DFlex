using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyPelvis : EnemyBodyPart
    {
        [field: SerializeField] 
        private ConfigurableJoint mainJoint;

        private int _currentHp;
        
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
        protected override  void Awake()
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
                bloodParent.SetActive(true);
                
                mainJoint.slerpDrive = _jointSpring;
                
                for (int i = 0; i < joints.Length; i++)
                    if (joints[i].TryGetComponent(out ConfigurableJoint joint))
                        ReleaseJoints(joint);
                
                if(enemyBodyParts != null)
                {
                    for (int i = 0; i < enemyBodyParts.Length; i++)
                    {
                        if (enemyBodyParts[i].GetComponent<PhysicalBodyPart>())
                        {
                            enemyBodyParts[i].GetComponentInChildren<PhysicalBodyPart>().RemoveTarget();
                            enemyBodyParts[i].GetComponent<ConfigurableJoint>().slerpDrive = _jointSpring;
                        }
                    }
                }
            }
        }
    }
}