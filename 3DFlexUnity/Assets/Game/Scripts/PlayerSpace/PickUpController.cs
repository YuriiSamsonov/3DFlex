using System;
using Game.Scripts.Objects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
        private float pickUpDistance = 2f;
        
        [field: SerializeField] 
        private float throwDistance;
        
        private CupMono _cupMono;
        
        private Rigidbody _objectInHandBody;

        private readonly RaycastHit[] _rayHits = new RaycastHit[1];
        
        private bool _foundCup;

        private void Awake()
        {
            _cupMono = FindObjectOfType<CupMono>();
        }

        private void Update()
        {
            _foundCup = TryCastForCup(cameraTransform.position, cameraTransform.forward);
        }

        public void OnInteractButton(InputAction.CallbackContext context)
        {
            if (!_cupMono.IsInHand)
            {
                if (_foundCup)
                {
                    _cupMono = _rayHits[0].collider.GetComponent<CupMono>();
                    _cupMono.Grab(hand);
                }
            } 
            else 
                _cupMono.Drop();
        }

        public void OnThrowButton(InputAction.CallbackContext context)
        {
            if (_cupMono.IsInHand)
            {
                _cupMono.Drop();
                ThrowObjectInHand();
            }
        }

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