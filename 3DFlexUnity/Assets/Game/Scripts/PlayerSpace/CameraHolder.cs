using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class CameraHolder : MonoBehaviour
    {
        /// <summary>
        /// Camera position.
        /// </summary>
        [field: SerializeField, Tooltip("Camera position.")] 
        private Transform cameraTransform;

        private void LateUpdate()
        {
            transform.position = cameraTransform.position;
        }
    }
}