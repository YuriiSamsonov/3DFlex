using System.Collections;
using UnityEngine;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public abstract class EnemyBodyPart : MonoBehaviour
    {
        /// <summary>
        /// Copy moving of the YellowDude body part moving
        /// </summary>
        [field: SerializeField, Tooltip("Copy moving of the YellowDude body part moving")] 
        protected PhysicalBodyPart[] enemyBodyParts;

        /// <summary>
        /// Joints of the enemy body parts
        /// </summary>
        [field: SerializeField, Tooltip("Joints of the enemy body parts")] 
        protected ConfigurableJoint[] joints;
        
        /// <summary>
        /// Renderer of enemy body part.
        /// Using for visualize when damage taken.
        /// </summary>
        [field: SerializeField, Tooltip("Renderer of enemy body part")] 
        protected Renderer bpRenderer;
        
        /// <summary>
        /// Max HP of the enemy
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Max HP of the enemy")] 
        protected int partMaxHealth;

        /// <summary>
        /// Activates when part comes off the body
        /// </summary>
        [field: SerializeField, Tooltip("Activates when part comes off the body")] 
        protected GameObject bloodParent;
        
        /// <summary>
        /// Joint which keeps enemy in vertical position.
        /// </summary>
        [field: SerializeField, Tooltip("Joint which keeps enemy in vertical position.")] 
        private ConfigurableJoint mainJoint;

        private Color _defaultColor;
        
        public bool isDead;
        public int currentHp;
        
        private readonly JointDrive _jointSpring = new(){ positionSpring = 0f, positionDamper = 0f };

        protected virtual void Awake()
        {
            currentHp = partMaxHealth;
            _defaultColor = bpRenderer.material.color;
        }

        /// <summary>
        /// Notifies the body part that is was hit for certain amount fo damage.
        /// Based on the inheriting part either only discards the part itself or all child body parts as well.
        /// </summary>
        /// <param name="damage"></param>
        public virtual void OnHit(int damage)
        {
            currentHp = Mathf.Max(0, currentHp - damage);
            
            var material = bpRenderer.material;
            material.color = Color.red;

            const float delay = 1.0f;
            StartCoroutine(ResetColorAfterDelay(delay, material));
            
            bloodParent.SetActive(true);

            if (currentHp <= 0)
            {
                ReleaseJoints();
                DisabledPhysicalBodyPart();
            }
        }
        
        private IEnumerator ResetColorAfterDelay(float delay, Material material)
        {
            yield return new WaitForSeconds(delay);
            material.color = _defaultColor;
        }

        /// <summary>
        /// Releases joint moving and disconnects from the enemy body.
        /// </summary>
        private void ReleaseJoints()
        {
            for (int i = 0; i < joints.Length; i++)
            {
                joints[i].connectedBody = null;
                joints[i].slerpDrive = _jointSpring;
                joints[i].xMotion = joints[i].yMotion = joints[i].zMotion = ConfigurableJointMotion.Free;
            }
        }

        /// <summary>
        /// Disable all enemyBodyParts.
        /// </summary>
        private void DisabledPhysicalBodyPart()
        {
            for (int i = 0; i < enemyBodyParts.Length; i++)
                enemyBodyParts[i].enabled = false;
        }

        /// <summary>
        /// Set mainJoint.slerpDrive to zero value.
        /// </summary>
        protected void ReleaseMainJoint()
        {
            mainJoint.slerpDrive = _jointSpring;
        }
    }
}