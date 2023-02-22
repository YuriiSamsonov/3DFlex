using System.Collections;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class GrabableObject : MonoBehaviour
    {
        [field: SerializeField] 
        private Rigidbody rBody;
        
        [field: SerializeField] 
        private GameObject brokenCup;
        
        [field: SerializeField] 
        private Transform spawnPoint;
        
        [field: SerializeField] 
        private int damage = 10;

        public Rigidbody RBody => rBody;

        private Transform _objInHandTransform;
        private Vector3 _lastPos;
        private Quaternion _lastRot;

        private bool _inHand;

        private readonly float _lerpSpeed = 10f;

        private void Reset()
        {
            rBody = GetComponent<Rigidbody>();
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

        private void FixedUpdate()
        {
            if (_objInHandTransform != null)
            {
                var moveLerp = 
                    Vector3.Lerp(transform.position, 
                    _objInHandTransform.position, 
                    Time.deltaTime * _lerpSpeed);
                
                rBody.MovePosition(moveLerp);
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

                var enemy = collision.collider.GetComponent<EnemyBody>();

                if (enemy)
                {
                    enemy.OnHit(damage);
                }

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