using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Scripts.PlayerSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        
        [field: SerializeField] 
        private Transform orientation;
        
        [field: SerializeField] 
        private float moveSpeed;
        
        [Header("Jump")]
        
        [field: SerializeField] 
        private float jumpForce;

        [field: SerializeField] 
        private float airMultiplier;

        private bool _readyToJump = true;

        [Space]
        [Header("GroundCheck")]
        
        [field: SerializeField] 
        private float groundDrag;
        
        [field: SerializeField] 
        private Transform rayStart;

        [field: SerializeField] 
        private LayerMask whatIsGround;
        
        [field: SerializeField] 
        private float rayLength;
        
        private bool _grounded;


        private Rigidbody _rBody;

        private Vector2 _horizontalInput;
        private Vector3 _moveDirection;
        private Vector3 _vel;

        private void Awake()
        {
            _rBody = GetComponent<Rigidbody>();
            _rBody.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Update()
        {
            SpeedControl();
            
             if (_grounded)
                 _rBody.drag = groundDrag;
             else
                 _rBody.drag = 0f;
        }

        private void Movement()
        {
            _moveDirection = orientation.forward * _horizontalInput.y + orientation.right * _horizontalInput.x;
            
            _grounded = Physics.Raycast(rayStart.position, Vector3.down, rayLength, whatIsGround);

            if (_grounded)
            {
                _rBody.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
                ResetJump();
            }

            else if (!_grounded)
                _rBody.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);

        }

        private void SpeedControl()
        {
            var velocity = _rBody.velocity;
            Vector3 flatVel = new Vector3(velocity.x, 0f, velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                _rBody.velocity = new Vector3(limitedVel.x, _rBody.velocity.y, limitedVel.z);
            }
        }

        public void OnJumpButton(InputAction.CallbackContext context)
        {
            if (_readyToJump && _grounded)
            {
                _readyToJump = false;
                
                Jump();
            }
        }

        private void Jump()
        {
            _rBody.velocity = new Vector3(_rBody.velocity.x, 0f, _rBody.velocity.z);

            _rBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        
        private void ResetJump()
        {
            _readyToJump = true;
        }

        public void ReceiveInput(Vector2 horizontalInput)
        {
            _horizontalInput = horizontalInput;
        }
    }
}