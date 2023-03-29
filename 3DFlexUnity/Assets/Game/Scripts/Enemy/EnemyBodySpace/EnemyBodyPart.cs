using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.Enemy.EnemyBodySpace
{
    public abstract class EnemyBodyPart : MonoBehaviour
    {
        /// <summary>
        /// Max HP of the enemy
        /// </summary>
        [field: SerializeField, Tooltip("Max HP of the enemy"), Min(1)]
        protected int partMaxHealth;

        /// <summary>
        /// Renderer of enemy body part.
        /// Using for visualize when damage taken.
        /// </summary>
        [field: SerializeField, Tooltip("Renderer of enemy body part")] 
        protected Renderer bpRenderer;

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
        /// Activates when part comes off the body
        /// </summary>
        [field: SerializeField, Tooltip("Activates when part comes off the body")] 
        protected GameObject bloodParent;

        private Color _defaultColor;

        protected virtual void Awake()
        {
            _defaultColor = bpRenderer.material.color;
        }

        /// <summary>
        /// Activates when body part damaged /// todo more correct naming?
        /// </summary>
        /// <param name="damage"></param>
        public abstract void OnHit(int damage);

        protected IEnumerator ResetColorAfterDelay(float delay, Material material)
        {
            yield return new WaitForSeconds(delay);
            material.color = _defaultColor;
        }

        /// <summary>
        /// Releases joint moving
        /// </summary>
        /// <param name="joint"></param>
        protected static void ReleaseJoints(ConfigurableJoint joint)
        {
            joint.connectedBody = null;
            joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Free;
        }
    }
}