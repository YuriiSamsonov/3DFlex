using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyHead : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp = 50;

        [field: SerializeField] 
        private ConfigurableJoint jointToDestroy;

        [field: SerializeField] 
        private GameObject[] partsToKill;

        [field: SerializeField] 
        private Renderer pelvisRenderer;
        
        private int _currentHp;
        private Color _currentColor;
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
        private void Awake()
        {
            _currentColor = pelvisRenderer.material.color;
            _currentHp = maxHp;
        }

        public void OnHit(int damage)
        {
            _currentHp -= damage;
            
            pelvisRenderer.material.color = Color.red;
            StartCoroutine(ResetColorWithSeconds());

            if (_currentHp <= 0)
            {
                jointToDestroy.connectedBody = null;
                jointToDestroy.slerpDrive = _jointSpring;
                jointToDestroy.yMotion = ConfigurableJointMotion.Free;
                jointToDestroy.zMotion = ConfigurableJointMotion.Free;
                jointToDestroy.yMotion = ConfigurableJointMotion.Free;
                
                for (int i = 0; i < partsToKill.Length; i++)
                {
                    if(partsToKill[i].GetComponent<PhysicalBodyPart>())
                        partsToKill[i].GetComponent<PhysicalBodyPart>().RemoveTarget();
                    
                    if (partsToKill[i].GetComponent<ConfigurableJoint>())
                    {
                        StartCoroutine(KillPartsWithSeconds(i));
                    }
                }
            }
        }

        private IEnumerator ResetColorWithSeconds()
        {
            yield return new WaitForSeconds(1);
            pelvisRenderer.material.color = _currentColor;
        }
        
        private IEnumerator KillPartsWithSeconds(int i)
        {
            yield return new WaitForSeconds(1);
            partsToKill[i].GetComponent<ConfigurableJoint>().slerpDrive = _jointSpring;
        }
    }
}