using UnityEngine;

namespace Game.Scripts.Enemy
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class PhysicalBodyPart : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform target;

        private ConfigurableJoint _joint;
        private Quaternion _startRotation;

        private void Start()
        {
            _joint = GetComponent<ConfigurableJoint>();
            _startRotation = transform.localRotation;
        }

        private void FixedUpdate()
        {
            if (target != null)
                _joint.targetRotation = Quaternion.Inverse(target.localRotation) * _startRotation;
        }

        public void RemoveTarget()
        {
            target = null;
        }
    }
}