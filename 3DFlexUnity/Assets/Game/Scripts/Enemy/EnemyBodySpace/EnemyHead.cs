using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyHead : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform target;
        
        [field: SerializeField] 
        private int maxHp = 50;
        
        [field: SerializeField] 
        private Rigidbody rBody;

        [field: SerializeField] 
        private ConfigurableJoint jointToDestroy;
        
        [field: SerializeField] 
        private GameObject blood;

        [field: SerializeField] 
        private GameObject[] partsToKill;
        
        [field: SerializeField] 
        private GameObject[] allParts;

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
                blood.SetActive(true);
                
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

                for (int i = 0; i < allParts.Length; i++)
                {

                    if (allParts[i].GetComponent<EnemyChest>())
                    {
                        allParts[i].GetComponent<EnemyChest>().OnHit(100000);
                    }
                    
                    StartCoroutine(DestroyTrash(i));

                    allParts[i].tag = "Untagged";
                    
                    if (allParts[i].GetComponent<EnemyArm>())
                        Destroy(allParts[i].GetComponent<EnemyArm>());

                    if (allParts[i].GetComponent<EnemyChest>())
                        Destroy(allParts[i].GetComponent<EnemyChest>());

                    if (allParts[i].GetComponent<EnemyLeg>())
                        Destroy(allParts[i].GetComponent<EnemyLeg>());
                    
                    if (allParts[i].GetComponent<EnemyPelvis>())
                        Destroy(allParts[i].GetComponent<EnemyPelvis>());
                    
                    if (allParts[i].GetComponent<DirectionController>())
                        Destroy(allParts[i].GetComponent<DirectionController>());
                }
            }
        }

        private IEnumerator DestroyTrash(int i)
        {
            yield return new WaitForSeconds(30);
            allParts[i].SetActive(false);
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