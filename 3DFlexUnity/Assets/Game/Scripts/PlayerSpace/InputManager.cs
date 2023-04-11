using Game.Scripts.UI;
using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// PlayerMovement.cs reference.            ?????
        /// </summary>
        [field: SerializeField, Tooltip("PlayerMovement.cs reference.")]     
        private PlayerMovement playerMovement;
        
        /// <summary>
        /// MouseLook.cs reference.
        /// </summary>
        [field: SerializeField, Tooltip("MouseLook.cs reference.")] 
        private MouseLook mouseLook;
        
        /// <summary>
        /// PickUpController.cs reference.
        /// </summary>
        [field: SerializeField, Tooltip("PickUpController.cs reference.")] 
        private PickUpController pickUpController;
        
        /// <summary>
        /// UIManager reference.
        /// </summary>
        [field: SerializeField, Tooltip("UIManager reference."), Space] 
        private UIManager uiManager;

        private PlayerControls _playerControls;
        private PlayerControls.GroundMovementActions _groundMovement;

        private Vector2 _horizontalInput;
        private Vector2 _mouseInput;

        private void Awake()
        {
            _playerControls = new PlayerControls();
            _groundMovement = _playerControls.GroundMovement;
            
            //_groundMovement.[action].performed += context => do something
            
            _groundMovement.HorizontalMovement.performed += ctx => 
                _horizontalInput = ctx.ReadValue<Vector2>();
            
            _groundMovement.MouseX.performed += ctx =>
                _mouseInput.x = ctx.ReadValue<float>();
            
            _groundMovement.MouseY.performed += ctx =>
                _mouseInput.y = ctx.ReadValue<float>();
            
            _groundMovement.Jump.performed += playerMovement.OnJumpButton;
            _groundMovement.Interact.performed += pickUpController.OnInteractButton;
            _groundMovement.Throw.performed += pickUpController.OnThrowButton;
            _groundMovement.Escape.performed += uiManager.OnEscapeButton;
        }

        private void Update()
        {
            playerMovement.ReceiveInput(_horizontalInput);
            mouseLook.ReceiveInput(_mouseInput);
        }

        private void OnEnable()
        {
            _playerControls.GroundMovement.Enable();
        }

        private void OnDisable()
        {
            _playerControls.GroundMovement.Disable();
        }
    }
}