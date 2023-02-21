using System;
using Game.Scripts.Objects;
using UnityEngine;

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

        private GrabableObject _objectInHand;
        private Rigidbody _objectInHandBody;

        private RaycastHit[] _rayHits = new RaycastHit[1];

        private bool _holdingItem;
        private bool _foundCup;

        private void FixedUpdate()
        {
            if (_holdingItem && Input.GetKey(KeyCode.Mouse0))
            {
                OnThrowButton();
            }
        }

        private void Update()
        {
            _foundCup = TryCastForCup(cameraTransform.position, cameraTransform.forward);
            
            if (!_holdingItem && Input.GetKeyDown(KeyCode.E))
            {
                if (_foundCup)
                {
                    _objectInHand = _rayHits[0].collider.GetComponent<GrabableObject>();
                    _objectInHand.Grab(hand);
                    _objectInHand.InHandState(true);
                    _objectInHandBody = _objectInHand.RBody;
                    _holdingItem = true;
                    return;
                }
            }
            
            if(_holdingItem && Input.GetKeyDown(KeyCode.E))
            {
                _objectInHand.Drop();
                _objectInHand.InHandState(false);
                _holdingItem = false;
            }
        }

        private void OnThrowButton()
        {
            _objectInHand.Drop();
            _objectInHand.InHandState(false);
            ThrowObjectInHand();
            _objectInHandBody = null;
            _holdingItem = false;
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