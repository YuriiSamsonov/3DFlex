using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyLeg : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp = 50;
        
        [field: SerializeField] 
        private ConfigurableJoint mainJoint;

        [field: SerializeField] 
        private GameObject jointObject;
        
        [field: SerializeField] 
        private GameObject blood;
        
        [field: SerializeField] 
        private GameObject[] partsToKill;

        [field: SerializeField] 
        private Renderer[] legRenderer;
        
        [field: SerializeField] 
        private bool rightLeg;
        
        [field: SerializeField] 
        private BoxCollider colToChangeMaterial;

        public BoxCollider ColToChangeMaterial => colToChangeMaterial;
        
        public bool RightLeg => rightLeg;
        
        private int _currentHp;
        private Color _currentColor;
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
        private void Awake()
        {
            _currentColor = legRenderer[0].material.color;
            _currentHp = maxHp;
        }
        
        public void OnHit(int damage)
        {
            _currentHp -= damage;

            for (int i = 0; i < legRenderer.Length; i++)
            {
                legRenderer[i].material.color = Color.red;
                StartCoroutine(ResetColorWithSeconds(i));
            }

            if (_currentHp <= 0)
            {
                Destroy(jointObject.GetComponent<PhysicalBodyPart>());
                Destroy(jointObject.GetComponent<ConfigurableJoint>());
                mainJoint.slerpDrive = _jointSpring;
                
                blood.SetActive(true);
                
                for (int i = 0; i < partsToKill.Length; i++)
                {
                    if (partsToKill[i].GetComponent<PhysicalBodyPart>())
                    {
                        partsToKill[i].GetComponentInChildren<PhysicalBodyPart>().RemoveTarget();
                        partsToKill[i].GetComponent<ConfigurableJoint>().slerpDrive = _jointSpring;
                    }
                }
                
            }
        }

        private IEnumerator ResetColorWithSeconds(int i)
        {
            yield return new WaitForSeconds(1);
            legRenderer[i].material.color = _currentColor;
        }
    }
}