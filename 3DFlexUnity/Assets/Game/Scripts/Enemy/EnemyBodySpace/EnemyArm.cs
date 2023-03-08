using System;
using System.Collections;
using Game.Scripts.PlayerSpace;
using Game.Scripts.Utils;
using UnityEngine;

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
        
        [field: SerializeField] 
        private GameObject blood;

        [field: SerializeField]
        private PlayerMono playerMono;
        
        private int _currentHp;
        private int _damage = 1;

        private bool _isAlive = true;
        
        private Color _currentColor;

        private void Awake()
        {
            _currentColor = armRenderer.material.color;
            _currentHp = maxHp;
        }

        private void Start()
        {
            playerMono = FindObjectOfType<PlayerMono>();
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(playerMono.transform.position, transform.position) <= 1.1f && _isAlive)
            {
                playerMono.OnHit(_damage);
            }
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
                blood.SetActive(true);
                _isAlive = false;
            }
        }

        private IEnumerator ResetColorWithSeconds()
        {
            yield return new WaitForSeconds(1);
            armRenderer.material.color = _currentColor;
        }
    }
}