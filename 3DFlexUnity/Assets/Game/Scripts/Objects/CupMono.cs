using System.Collections;
using Game.Scripts.Enemy.EnemyBodySpace;
using Game.Scripts.ScriptableObjects;
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
        private GameObject brokenCup;
        
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

        private bool _inHand;

        private readonly float _lerpSpeed = 20f;

        private void Reset()
        {
            rBody = GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            _cupRenderer = GetComponent<Renderer>();
            _cupRenderer.material.mainTexture = cupRuntimeData.cupTexture;
        }

        public void Grab(Transform handTransform)
        {
            _objInHandTransform = handTransform;
            rBody.drag = 5f;
            rBody.useGravity = false; 
            rBody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        public void Drop()
        {
            _objInHandTransform = null;
            rBody.drag = 0f;
            rBody.useGravity = true;
            rBody.constraints = RigidbodyConstraints.None;
        }

        private void OnCollision()
        {
            var newBrokenCup = Instantiate(brokenCup);

            var currentTransform = transform;
            newBrokenCup.transform.position = currentTransform.position;
            newBrokenCup.transform.rotation = currentTransform.rotation;
                
            rBody.velocity = new Vector3(0,0,0);
            currentTransform.rotation = new Quaternion(0, 0, 0, 0);
                
            currentTransform.position = spawnPoint.position;
            
            StartCoroutine(DestroyTrash(newBrokenCup));
        }

        private void Update()
        {
            if (_objInHandTransform != null)
            {
                var moveLerp = 
                    Vector3.Lerp(transform.position, 
                    _objInHandTransform.position, 
                    Time.deltaTime * _lerpSpeed);
                
                rBody.MovePosition(moveLerp);
            }

            if (Vector3.Distance(spawnPoint.position, transform.position) > 100)
            {
                OnCollision();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (rBody.velocity.magnitude > 2.5f && !_inHand)
            {
                OnCollision();

                switch (collision.collider.tag)
                {
                    case "EnemyArm":
                        collision.collider.GetComponent<EnemyArm>().OnHit(damage);
                        break;
                    case "EnemyHead":
                        collision.collider.GetComponent<EnemyHead>().OnHit(damage);
                        break;
                    case "EnemyLeg":
                        if (collision.collider.GetComponent<EnemyLeg>())
                            collision.collider.GetComponent<EnemyLeg>().OnHit(damage);
                        else
                            collision.collider.GetComponentInParent<EnemyLeg>().OnHit(damage); //bug
                        break;
                    case "EnemyChest":
                        collision.collider.GetComponent<EnemyChest>().OnHit(damage);
                        break;
                    case "EnemyPelvis":
                        collision.collider.GetComponent<EnemyPelvis>().OnHit(damage);
                        break;
                }
            }
        }

        public void InHandState(bool state)
        {
            _inHand = state;
        }

        private IEnumerator DestroyTrash(GameObject gameObjectToDestroy)
        {
            yield return new WaitForSeconds(30);
            gameObjectToDestroy.SetActive(false);
            Destroy(gameObjectToDestroy);
        }
    }
}