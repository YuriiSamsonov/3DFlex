using UnityEngine;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class PhysicalBodyPart : MonoBehaviour
    {
        private Transform _target;
        private ConfigurableJoint _joint;
        private Quaternion _startRotation;

        /// <summary>
        /// Initialize PhysicalBodyPart components and finds the joint attached to the same game object.
        /// </summary>
        /// <param name="target"></param>
        public void Init(Transform target)
        {
            _target = target;
            _joint = GetComponent<ConfigurableJoint>();
            _startRotation = transform.localRotation;
        }

        private void FixedUpdate()
        {
            _joint.targetRotation = Quaternion.Inverse(_target.localRotation) * _startRotation;
        }
    }
}