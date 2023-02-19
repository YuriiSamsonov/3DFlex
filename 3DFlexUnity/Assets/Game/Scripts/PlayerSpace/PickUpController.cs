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
        private float pickUpDistance = 2f;
        
        [field: SerializeField] 
        private float throwDistance;

        private GrabableObject _objectInHand;
        private Rigidbody _objectInHandBody;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_objectInHand == null)
                {
                    Physics.Raycast(cameraTransform.position, 
                        cameraTransform.forward, 
                        out RaycastHit raycastHit, 
                        pickUpDistance,
                        pickUpLayerMask);

                    if (raycastHit.transform != null && raycastHit.transform.TryGetComponent(out GrabableObject grabableObject))
                    {
                        _objectInHand = grabableObject;
                        _objectInHand.Grab(hand);
                        _objectInHandBody = grabableObject.RBody;
                    }
                }
                else

                {
                    _objectInHand.Drop();
                    _objectInHandBody.velocity = cameraTransform.forward * (throwDistance * 10 * Time.deltaTime);
                    _objectInHand = null;
                }

            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            }
        }
    }
}