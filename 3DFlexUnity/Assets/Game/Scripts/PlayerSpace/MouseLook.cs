using System.Collections;
using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class MouseLook : MonoBehaviour
    {
        /// <summary>
        /// Horizontal mouse moving multiplier.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Horizontal mouse moving multiplier.")] 
        private float sensitivityX = 10f;
        
        /// <summary>
        /// Vertical mouse moving multiplier.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Vertical mouse moving multiplier.")] 
        private float sensitivityY = 10f;
        
        /// <summary>
        /// Player component responsible for rotation.
        /// </summary>
        [field: SerializeField, Tooltip("Player component responsible for rotation.")] 
        private Transform orientation;

        private float _mouseX;
        private float _mouseY;
        private float _xRotation;
        private float _yRotation;
        
        private float _clock;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            StartCoroutine(UnlockMouseWithDelay(0.5f));
            enabled = false;
        }

        private void Update()
        {
            _yRotation += _mouseX;
            _xRotation -= _mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
                
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }

        public void ReceiveInput(Vector2 mouseInput)
        {
            _mouseX = mouseInput.x * Time.deltaTime * sensitivityX;
            _mouseY = mouseInput.y * Time.deltaTime * sensitivityY;
        }

        private IEnumerator UnlockMouseWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            enabled = true;
        }
    }
}