using System.Collections;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyBody : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp = 50;

        [field: SerializeField] 
        private GameObject jointObject;

        [field: SerializeField] 
        private GameObject controller;

        [field: SerializeField] 
        private Renderer renderer;
        
        [field: SerializeField] 
        private GameObject[] objectsToDestroy;

        private int _currentHp;
        private Color _currentColor;
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };

        private void Awake()
        {
            _currentColor = renderer.material.color;
            _currentHp = maxHp;
        }

        public void OnHit(int damage)
        {
            _currentHp -= damage;
            
            renderer.material.color = Color.red;
            StartCoroutine(ResetColorWithSeconds());

            if (_currentHp <= 0)
            {
                for (int i = 0; i < objectsToDestroy.Length; i++)
                {
                    Destroy(objectsToDestroy[i].GetComponent<PhysicalBodyPart>());
                    objectsToDestroy[i].GetComponent<ConfigurableJoint>().slerpDrive = _jointSpring;
                    
                    Destroy(objectsToDestroy[i].GetComponent<EnemyBody>());
                }
                Destroy(jointObject.GetComponent<PhysicalBodyPart>());
                Destroy(jointObject.GetComponent<ConfigurableJoint>());
                Destroy(controller.gameObject);
            }
        }

        private IEnumerator ResetColorWithSeconds()
        {
            yield return new WaitForSeconds(1);
            renderer.material.color = _currentColor;
        }
    }
}