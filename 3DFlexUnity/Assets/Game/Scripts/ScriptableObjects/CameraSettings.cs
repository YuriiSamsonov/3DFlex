using UnityEngine;

namespace Game.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data/CreateCameraSettings", fileName = "new CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [field: SerializeField] 
        private float sensitivityX = 8f;
        /// <summary>
        /// X axis sensitivity  
        /// </summary>
        public float SensitivityX => sensitivityX;
        
        [field: SerializeField] 
        private float sensitivityY = 0.5f;
        /// <summary>
        /// Y axis sensitivity
        /// </summary>
        public float SensitivityY => sensitivityY;
        
        [field: SerializeField] 
        private float xClamp = 90f;
        /// <summary>
        /// Limits X rotation
        /// </summary>
        public float XClamp => xClamp;
    }
}