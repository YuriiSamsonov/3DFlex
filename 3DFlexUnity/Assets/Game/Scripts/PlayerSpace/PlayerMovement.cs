using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.PlayerSpace
{
    public class PlayerMovement : MonoBehaviour
    {
        [field: SerializeField] 
        private CharacterController controller;
        
        [field: SerializeField] 
        private float speed;
        
        [field: SerializeField] 
        private float gravity = -9.81f;
        
        [field: SerializeField] 
        private Transform groundCheck;
        
        [field: SerializeField] 
        private float groundDistance = 0.4f;
        
        [field: SerializeField] 
        private LayerMask groundMask;
        
        [field: SerializeField] 
        private float jumpHeight = 3f;
        
        private float _x;
        private float _z;

        private bool _isGrounded;

        private Vector3 _velocity;

        private void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
            
            _x = Input.GetAxis("Horizontal");
            _z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * _x + transform.forward * _z;

            controller.Move(move * (speed * Time.deltaTime));

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            if (!_isGrounded)
            {
                _velocity.y -= Time.deltaTime * 10;
            }

            _velocity.y += gravity * Time.deltaTime;

            controller.Move(_velocity * Time.deltaTime);
        }
    }
}