using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyLeg : EnemyBodyPart
    {
        [field: SerializeField] 
        private ConfigurableJoint mainJoint;
        
        [field: SerializeField] 
        private Renderer[] legRenderer;
        
        [field: SerializeField] 
        private bool rightLeg;
        
        [field: SerializeField] 
        private BoxCollider colToChangeMaterial;

        public BoxCollider ColToChangeMaterial => colToChangeMaterial;
        
        public bool RightLeg => rightLeg;
        
        private int _currentHp;
        
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
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
                joints[0].GetComponent<PhysicalBodyPart>().enabled = false;
                ReleaseJoints(joints[0]);
                mainJoint.slerpDrive = _jointSpring;
                
                bloodParent.SetActive(true);
                
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