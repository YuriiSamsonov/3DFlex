using System;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class CameraHolder : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform cameraTransform;

        private void Update()
        {
            transform.position = cameraTransform.position;
        }
    }
}