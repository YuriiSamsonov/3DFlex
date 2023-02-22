using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyPelvis : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp = 50;

        [field: SerializeField] 
        private ConfigurableJoint[] jointsToDestroy;

        [field: SerializeField] 
        private GameObject[] partsToKill;

        [field: SerializeField] 
        private Renderer[] pelvisRenderer;
        
        private int _currentHp;
        private Color _currentColor;
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };
        
        private void Awake()
        {
            _currentColor = pelvisRenderer[0].material.color;
            _currentHp = maxHp;
        }

        public void OnHit(int damage)
        {
            _currentHp -= damage;

            for (int i = 0; i < pelvisRenderer.Length; i++)
            {
                pelvisRenderer[i].material.color = Color.red;
                StartCoroutine(ResetColorWithSeconds(i));
            }

            if (_currentHp <= 0)
            {
                for (int i = 0; i < jointsToDestroy.Length; i++)
                {
                    if (jointsToDestroy[i].GetComponent<ConfigurableJoint>())
                    {
                        ReleaseJoints(i);
                    }

                }
                
                for (int i = 0; i < partsToKill.Length; i++)
                {
                    if (partsToKill[i].GetComponent<PhysicalBodyPart>())
                    {
                        partsToKill[i].GetComponentInChildren<PhysicalBodyPart>().RemoveTarget();
                        StartCoroutine(KillPartsWithSeconds(i));
                    }
                }
            }
        }

        private void ReleaseJoints(int i)
        {
            var joint = jointsToDestroy[i].GetComponent<ConfigurableJoint>(); 
            joint.connectedBody = null;
            joint.yMotion = ConfigurableJointMotion.Free;
            joint.zMotion = ConfigurableJointMotion.Free;
        }

        private IEnumerator ResetColorWithSeconds(int i)
        {
            yield return new WaitForSeconds(1);
            pelvisRenderer[i].material.color = _currentColor;
        }
        
        private IEnumerator KillPartsWithSeconds(int i)
        {
            yield return new WaitForSeconds(1);
            partsToKill[i].GetComponent<ConfigurableJoint>().slerpDrive = _jointSpring;
            partsToKill[i].GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Impulse);
        }
    }
}