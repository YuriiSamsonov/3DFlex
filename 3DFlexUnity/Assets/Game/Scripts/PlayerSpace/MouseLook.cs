using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class MouseLook : MonoBehaviour
    {
        private float _mouseX, _mouseY;
        
        [field: SerializeField] private float sensitivityX = 400f;
        [field: SerializeField] private float sensitivityY = 400f;
        [field: SerializeField] private Transform orientation;

        private float _xRotation;
        private float _yRotation;
        
        private float _clock;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _clock += Time.deltaTime;
            
            if (_clock > 0.5f) //I did it because Logitech MX3 qualitative mouse
            {
                _yRotation += _mouseX;
                _xRotation -= _mouseY;
                _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
                
                transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
                orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
            }
        }

        public void ReceiveInput(Vector2 mouseInput)
        {
            _mouseX = mouseInput.x * Time.deltaTime * sensitivityX;
            _mouseY = mouseInput.y * Time.deltaTime * sensitivityY;
        }
    }
}