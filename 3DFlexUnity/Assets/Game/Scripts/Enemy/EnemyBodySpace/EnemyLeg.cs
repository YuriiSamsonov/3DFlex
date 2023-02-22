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
        private GameObject jointObject;

        [field: SerializeField] 
        private Renderer[] legRenderer;
        
        private int _currentHp;
        private Color _currentColor;
        
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
            }
        }

        private IEnumerator ResetColorWithSeconds(int i)
        {
            yield return new WaitForSeconds(1);
            legRenderer[i].material.color = _currentColor;
        }
    }
}