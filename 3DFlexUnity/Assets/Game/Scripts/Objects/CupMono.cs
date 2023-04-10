using System.Collections;
using Game.Scripts.Enemy.EnemyBodySpace;
using Game.Scripts.ScriptableObjects;
using Game.Scripts.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Objects
{
    public class CupMono : MonoBehaviour
    {
        [field: SerializeField] 
        private Rigidbody rBody;
        public Rigidbody RBody => rBody;
        
        [field: SerializeField] 
        private GameObject brokenCupPrefab;
        
        [field: SerializeField]
        private Transform spawnPoint;
        
        [field: SerializeField] 
        private int damage = 10;

        private Renderer _cupRenderer;
        
        [field: SerializeField]
        private CupRuntimeData cupRuntimeData;

        private Transform _objInHandTransform;
        private Vector3 _lastPos;
        private Quaternion _lastRot;
        private Transform _currentTransform;
        
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
            if (IsInHand)
            {
                var moveLerp = 
                    Vector3.Lerp(transform.position, 
                    _objInHandTransform.position, 
                    Time.deltaTime * LerpSpeed);
                
                rBody.MovePosition(moveLerp);
            }

            if (Vector3.Distance(spawnPoint.position, transform.position) > 100)
            {
                OnCollision();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsInHand)
            {
                if (collision.transform.CompareTag(Variables.EnemyBodyPart) || rBody.velocity.magnitude > 2.5f)
                {
                    OnCollision();
                    if(collision.collider.TryGetComponent<EnemyBodyPart>(out var part))
                        part.OnHit(damage);
                }
            }
        }

        public void Grab(Transform handTransform)
        {
            _objInHandTransform = handTransform;
            rBody.drag = 5f;
            rBody.useGravity = false; 
            rBody.constraints = RigidbodyConstraints.FreezeRotation;
            IsInHand = true;
        }

        public void Drop()
        {
            _objInHandTransform = null;
            rBody.drag = 0f;
            rBody.useGravity = true;
            rBody.constraints = RigidbodyConstraints.None;
            IsInHand = false;
        }

        private void OnCollision()
        {
            var originalT = transform;
            var newBrokenCup = Instantiate(brokenCupPrefab, originalT.position, originalT.rotation);
            
            rBody.velocity = Vector3.zero;
            originalT.rotation = Quaternion.identity;
            originalT.position = spawnPoint.position;
            
            StartCoroutine(DestroyTrash(newBrokenCup));
        }


        private IEnumerator DestroyTrash(GameObject gameObjectToDestroy)
        {
            yield return new WaitForSeconds(30);
            gameObjectToDestroy.SetActive(false);
            Destroy(gameObjectToDestroy);
        }
    }
}