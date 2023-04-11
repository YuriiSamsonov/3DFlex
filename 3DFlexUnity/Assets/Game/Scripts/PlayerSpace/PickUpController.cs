using Game.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.PlayerSpace
{
    public class PickUpController : MonoBehaviour
    {
        /// <summary>
        /// Transform of the camera.
        /// </summary>
        [field: SerializeField, Tooltip("Transform of the camera.")] 
        private Transform cameraTransform;
        
        /// <summary>
        /// Position to which the cup will move when taken.
        /// </summary>
        [field: SerializeField, Tooltip("Position to which the cup will move when taken.")] 
        private Transform hand;
        
        /// <summary>
        /// Layer on which there are all pickupable objects.
        /// </summary>
        [field: SerializeField, Tooltip("Layer on which there are all pickupable objects.")] 
        private LayerMask pickUpLayerMask;
        
        /// <summary>
        /// Distance at which the player can pick up an object.
        /// </summary>
        [field: SerializeField, Min(0.1f), Tooltip("Distance at which the player can pick up an object.")] 
        private float pickUpDistance = 2f;
        
        /// <summary>
        /// Distance that the thrown object will be moved.
        /// </summary>
        [field: SerializeField, Tooltip("Distance that the thrown object will be moved.")] 
        private float throwDistance;
        
        private CupMono _cupMono;
        
        private Rigidbody _objectInHandBody;

        private readonly RaycastHit[] _rayHits = new RaycastHit[1];
        
        private bool _foundCup;

        private void Awake()
        {
            _cupMono = FindObjectOfType<CupMono>();
        }

        /// <summary>
        /// On interact button pressed finds grabable object.
        /// If find it and player is not holding an object, moves to the hand position.
        /// If player already holding an object, releases it.
        /// </summary>
        /// <param name="context"></param>
        public void OnInteractButton(InputAction.CallbackContext context)
        {
            if (!_cupMono.IsInHand)
            {
                if (TryCastForCup(cameraTransform.position, cameraTransform.forward))
                {
                    _cupMono = _rayHits[0].collider.GetComponent<CupMono>();
                    _cupMono.Grab(hand);
                }
            } 
            else 
                _cupMono.Drop();
        }

        /// <summary>
        /// On throw button pressed throws object that player hold.
        /// </summary>
        /// <param name="context"></param>
        public void OnThrowButton(InputAction.CallbackContext context)
        {
            if (_cupMono.IsInHand)
            {
                _cupMono.Drop();
                ThrowObjectInHand();
            }
        }

        /// <summary>
        /// Throw object in hand.                 ????????????
        /// </summary>
        private void ThrowObjectInHand()
        {
            _cupMono.RBody.velocity = cameraTransform.forward * (throwDistance * 10 * Time.deltaTime);
        }
        
        private bool TryCastForCup(Vector3 startPos, Vector3 dir)
        {
            var hits = Physics.RaycastNonAlloc(startPos, dir, 
                _rayHits, 
                pickUpDistance, 
                pickUpLayerMask);

            return hits > 0;
        }
    }
}