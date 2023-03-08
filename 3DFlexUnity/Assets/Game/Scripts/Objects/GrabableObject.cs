using System.Collections;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class GrabableObject : MonoBehaviour
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

        [field: SerializeField] 
        private MainMenu mainMenu;
        
        private Renderer _cupRenderer;

        private Transform _objInHandTransform;
        private Vector3 _lastPos;
        private Quaternion _lastRot;

        private bool _inHand;

        private readonly float _lerpSpeed = 20f;

        private void Reset()
        {
            rBody = GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            _cupRenderer = GetComponent<Renderer>();
            _cupRenderer.material.mainTexture = mainMenu.CupTexture[MainMenu.CurrentTexture];
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

        private void LateUpdate()
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
                var newBrokenCup = Instantiate(brokenCup);
                
                newBrokenCup.transform.position = transform.position;
                newBrokenCup.transform.rotation = transform.rotation;
                
                rBody.velocity = new Vector3(0,0,0);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                
                transform.position = spawnPoint.position;

                StartCoroutine(DestroyTrash(newBrokenCup));
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (rBody.velocity.magnitude > 2.5f && !_inHand)
            {
                var newBrokenCup = Instantiate(brokenCup);
                
                newBrokenCup.transform.position = transform.position;
                newBrokenCup.transform.rotation = transform.rotation;
                
                rBody.velocity = new Vector3(0,0,0);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                
                transform.position = spawnPoint.position;

                StartCoroutine(DestroyTrash(newBrokenCup));

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

        private IEnumerator DestroyTrash(GameObject gameObject)
        {
            yield return new WaitForSeconds(30);
            gameObject.SetActive(false);
        }
    }
}