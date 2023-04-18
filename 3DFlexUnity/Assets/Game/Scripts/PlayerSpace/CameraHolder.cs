using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class CameraHolder : MonoBehaviour
    {
        /// <summary>
        /// Camera transform.
        /// </summary>
        [field: SerializeField, Tooltip("Camera transform.")] 
        private Transform cameraTransform;

        private void LateUpdate()
        {
            transform.position = cameraTransform.position;
        }
    }
}