using System;
using System.Collections;
using Game.Scripts.Enemy.EnemyBodySpace;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class CupMono : MonoBehaviour
    {
        [field: SerializeField, Tooltip("Rigidbody of the cup.")] 
        private Rigidbody rBody;
        /// <summary>
        /// Rigidbody of the cup.
        /// </summary>
        public Rigidbody RBody => rBody;
        
        /// <summary>
        /// Broken cup to spawn after cup broke.
        /// </summary>
        [field: SerializeField, Tooltip("Broken cup to spawn after cup broke.")] 
        private GameObject brokenCupPrefab;
        
        /// <summary>
        /// Cup spawn position.
        /// </summary>
        [field: SerializeField, Tooltip("Cup spawn position.")]
        private Transform spawnPoint;
        
        /// <summary>
        /// Amount of damage the cup deals to the enemy.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Amount of damage the cup deals to the enemy.")] 
        private int damage = 10;
        
        /// <summary>
        /// Data to store cup texture in runtime.
        /// </summary>
        [field: SerializeField, Tooltip("Data to store cup texture in runtime.")]
        private CupRuntimeData cupRuntimeData;

        private Renderer _cupRenderer;
        private Transform _objInHandTransform;
        private Vector3 _lastPos;
        private Quaternion _lastRot;
        private Transform _currentTransform;
        
        [field: NonSerialized]
        public bool IsInHand;

        private const float LerpSpeed = 20f;

        private void Reset()
        {
            rBody = GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            _cupRenderer = GetComponent<Renderer>();
            _cupRenderer.material.mainTexture = cupRuntimeData.cupTexture;
        }
        
        private void Update()
        {
            //Moves cup to player hand position.
            if (IsInHand)
            {
                var moveLerp = 
                    Vector3.Lerp(transform.position, 
                    _objInHandTransform.position, 
                    Time.deltaTime * LerpSpeed);
                
                rBody.MovePosition(moveLerp);
            }

            //Break cup if it to far from spawnPoint.
            if (Vector3.Distance(spawnPoint.position, transform.position) > 100)
            {
                BreakCup();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsInHand)
            {
                if (collision.transform.CompareTag(Variables.EnemyBodyPart) || rBody.velocity.magnitude > 2.5f)
                {
                    BreakCup();
                    if(collision.collider.TryGetComponent<EnemyBodyPart>(out var part))
                        part.OnHit(damage);
                }
            }
        }

        /// <summary>
        /// Change parameters of the cup's rigidbody so that it follows the player cursor.
        /// </summary>
        /// <param name="handTransform"></param>
        public void Grab(Transform handTransform)
        {
            _objInHandTransform = handTransform;
            rBody.useGravity = false; 
            rBody.constraints = RigidbodyConstraints.FreezeRotation;
            IsInHand = true;
        }

        /// <summary>
        /// Change parameters of the cup's rigidbody to the original.
        /// </summary>
        public void Drop()
        {
            _objInHandTransform = null;
            rBody.useGravity = true;
            rBody.constraints = RigidbodyConstraints.None;
            IsInHand = false;
        }

        /// <summary>
        /// Spawns broken cup on the collision point and move original cup to spawn point.
        /// </summary>
        private void BreakCup()
        {
            var originalT = transform;
            var newBrokenCup = Instantiate(brokenCupPrefab, originalT.position, originalT.rotation);
            
            rBody.velocity = Vector3.zero;
            originalT.rotation = Quaternion.identity;
            originalT.position = spawnPoint.position;
            
            StartCoroutine(DestroyBrokenCup(newBrokenCup));
        }
        
        private IEnumerator DestroyBrokenCup(GameObject objectToDestroy)
        {
            yield return new WaitForSeconds(30);
            Destroy(objectToDestroy);
        }
    }
}