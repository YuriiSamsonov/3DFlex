using System;
using Game.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.PlayerSpace
{
    public class PickUpController : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform cameraTransform;
        
        [field: SerializeField] 
        private Transform hand;
        
        [field: SerializeField] 
        private LayerMask pickUpLayerMask;
        
        [field: SerializeField] 
        private float pickUpDistance = 100f;
        
        [field: SerializeField] 
        private float throwDistance;

        private CupMono _objectInHand;
        private Rigidbody _objectInHandBody;

        private readonly RaycastHit[] _rayHits = new RaycastHit[1];

        private bool _holdingItem;
        private bool _foundCup;
        
        private void Update()
        {
            _foundCup = TryCastForCup(cameraTransform.position, cameraTransform.forward);
        }

        public void OnInteractButton(InputAction.CallbackContext context)
        {
            if (!_holdingItem)
            {
                if (_foundCup)
                {
                    _objectInHand = _rayHits[0].collider.GetComponent<CupMono>();
                    _objectInHand.Grab(hand);
                    _objectInHand.InHandState(true);
                    _objectInHandBody = _objectInHand.RBody;
                    _holdingItem = true;
                    return;
                }
            }
            
            if(_holdingItem)
            {
                _objectInHand.Drop();
                _objectInHand.InHandState(false);
                _holdingItem = false;
            }
        }

        public void OnThrowButton(InputAction.CallbackContext context)
        {
            if (_holdingItem)
            {
                _objectInHand.Drop();
                _objectInHand.InHandState(false);
                ThrowObjectInHand();
                _objectInHandBody = null;
                _holdingItem = false;
            }
        }

        private void ThrowObjectInHand()
        {
            _objectInHandBody.velocity = cameraTransform.forward * (throwDistance * 10 * Time.deltaTime);
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