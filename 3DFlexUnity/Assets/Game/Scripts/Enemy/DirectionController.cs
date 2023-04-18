using Game.Scripts.PlayerSpace;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class DirectionController : MonoBehaviour
    {
        /// <summary>
        /// The target towards which the joint is turning.
        /// </summary>
        [field: SerializeField, Tooltip("The target towards which the joint is turning.")] 
        private Transform target;

        /// <summary>
        /// Main joint of the enemy which keeps enemy in vertical position.
        /// </summary>
        [field: SerializeField, Tooltip("Main joint of the enemy which keeps enemy in vertical position.")]
        private ConfigurableJoint mainJoint;
        
        /// <summary>
        /// Transform of the enemy pelvis.
        /// </summary>
        [field: SerializeField, Tooltip("Transform of the enemy pelvis.")] 
        private Transform pelvisTransform;

        private void Start()
        {
            target = FindObjectOfType<PlayerMono>().transform;
        }

        private void FixedUpdate()
        {
            SetRotationToPlayer();
        }

        /// <summary>
        /// Set ConfigurableJoint rotation in player position.
        /// </summary>
        private void SetRotationToPlayer()
        {
            Vector3 toTarget = target.position - pelvisTransform.position;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
            Quaternion rotation = Quaternion.LookRotation(toTargetXZ);

            mainJoint.targetRotation = Quaternion.Inverse(rotation);
        }
    }
}