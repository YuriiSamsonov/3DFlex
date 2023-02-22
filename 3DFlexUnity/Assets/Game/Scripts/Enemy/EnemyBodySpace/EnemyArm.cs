using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public class EnemyArm : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp = 50;

        [field: SerializeField] 
        private GameObject jointObject;

        [field: SerializeField] 
        private Renderer armRenderer;
        
        private int _currentHp;
        private Color _currentColor;
        
        private void Awake()
        {
            _currentColor = armRenderer.material.color;
            _currentHp = maxHp;
        }
        
        public void OnHit(int damage)
        {
            _currentHp -= damage;
            
            armRenderer.material.color = Color.red;
            StartCoroutine(ResetColorWithSeconds());

            if (_currentHp <= 0)
            {
                Destroy(jointObject.GetComponent<PhysicalBodyPart>());
                Destroy(jointObject.GetComponent<ConfigurableJoint>());
            }
        }

        private IEnumerator ResetColorWithSeconds()
        {
            yield return new WaitForSeconds(1);
            armRenderer.material.color = _currentColor;
        }
    }
}