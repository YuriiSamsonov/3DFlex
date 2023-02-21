using System;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class DirectionController : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform target;

        [field: SerializeField] 
        private ConfigurableJoint joint;
        
        [field: SerializeField] 
        private Transform pelvisTransform;

        private void FixedUpdate()
        {
            Vector3 toTarget = target.position - pelvisTransform.position;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
            Quaternion rotation = Quaternion.LookRotation(toTargetXZ);

            joint.targetRotation = Quaternion.Inverse(rotation);
        }
    }
}