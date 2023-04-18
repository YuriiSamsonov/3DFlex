using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Scripts.PlayerSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Movement

        /// <summary>
        /// Player component responsible for rotation.
        /// </summary>
        [field: SerializeField, Tooltip("Player component responsible for rotation."),Header("Movement")]
        private Transform orientation;
       
        /// <summary>
        /// Speed of the player's movement.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Speed of the player movement.")] 
        private float moveSpeed;
        
        /// <summary>
        /// Drag multiplayer on the ground.
        /// </summary>
        [field: SerializeField, Tooltip("Drag multiplayer on the ground.")] 
        private float dragMultiplier;


        #endregion

        #region Jump

        /// <summary>
        /// Players jump force.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Players jump force."), Header("Jump")] 
        private float jumpForce;

        /// <summary>
        /// Speed multiplayer in the air.
        /// </summary>
        [field: SerializeField, Min(0), Tooltip("Moving multiplayer in air.")] 
        private float airMultiplier;

        private bool _isReadyToJump = true;

        #endregion

        #region GroundCheck
        
        /// <summary>
        /// Start position of the ray.
        /// </summary>
        [Header("GroundCheck")] 
        [field: SerializeField, Tooltip("Start position of the ray.")]
        private Transform rayStart;

        /// <summary>
        /// Layer on which there are ground.
        /// </summary>
        [field: SerializeField, Tooltip("Layer on which there are ground.")] 
        private LayerMask whatIsGround;
        
        /// <summary>
        /// Length of the ground check ray.
        /// </summary>
        [field: SerializeField, Min(0), Tooltip("Length of the ground check ray.")] 
        private float rayLength;
        
        private bool _isGrounded;

        #endregion

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

            _rBody.drag = _isGrounded ? dragMultiplier : 0f;
        }

        /// <summary>
        /// Calculate moving direction and checks if the player is grounded.
        /// Add force depending on if the player is grounded.
        /// </summary>
        private void Movement()
        {
            _moveDirection = orientation.forward * _horizontalInput.y + orientation.right * _horizontalInput.x;
            
            _isGrounded = Physics.Raycast(rayStart.position, Vector3.down, rayLength, whatIsGround);

            if (_isGrounded)
            {
                _rBody.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
                _isReadyToJump = true;
            }

            else if (!_isGrounded)
                _rBody.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);

        }

        /// <summary>
        /// Limits speed of the player if he tries to move to fast.
        /// </summary>
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

        /// <summary>
        /// On jump button pressed check if player ready to jump. If ready - jumps.
        /// </summary>
        /// <param name="context"></param>
        public void OnJumpButton(InputAction.CallbackContext context)
        {
            if (_isReadyToJump && _isGrounded)
            {
                _isReadyToJump = false;
                
                Jump();
            }
        }

        /// <summary>
        /// Adds vertical force to the player.
        /// </summary>
        private void Jump()
        {
            _rBody.velocity = new Vector3(_rBody.velocity.x, 0f, _rBody.velocity.z);

            _rBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        /// <summary>
        /// Receive input in PlayerMovement.cs
        /// </summary>
        /// <param name="horizontalInput"></param>
        public void ReceiveInput(Vector2 horizontalInput)
        {
            _horizontalInput = horizontalInput;
        }
    }
}