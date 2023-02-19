using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class MouseLook : MonoBehaviour
    {
        [field: SerializeField] 
        private float mouseSensitivity = 600f;

        [field: SerializeField] 
        private Transform playerBody;

        private float xRotation = 0f;
        float clock = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            clock += Time.deltaTime;
            if (clock > 1f) //hold input for 1 second to avoid my wireless shity logitech b.ug 
            {
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
            
        }
    }
}