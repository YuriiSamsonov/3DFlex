using UnityEngine;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class PhysicalBodyPart : MonoBehaviour
    {
        private Transform _target;

        private ConfigurableJoint _joint;
        private Quaternion _startRotation;

        public void Init(Transform target)
        {
            _target = target;
            _joint = GetComponent<ConfigurableJoint>();
            _startRotation = transform.localRotation;
        }

        private void FixedUpdate()
        {
            if (_target != null)
                _joint.targetRotation = Quaternion.Inverse(_target.localRotation) * _startRotation;
        }

        public void RemoveTarget()
        {
            _target = null;
        }
    }
}