using System.Collections.Generic;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class AnimateFriction : MonoBehaviour
    {
        /// <summary>
        /// Material with normal friction.
        /// </summary>
        [field: SerializeField, Tooltip("Material with normal friction.")] 
        private PhysicMaterial normalFriction;
        
        /// <summary>
        /// Material with zero friction.
        /// </summary>
        [field: SerializeField, Tooltip("Material with zero friction.")]
        private PhysicMaterial zeroFriction;

        private List<EnemyLeg> _leftColliders;
        private List<EnemyLeg> _rightColliders;

        private void Awake()
        {
            _leftColliders = new List<EnemyLeg>();
            _rightColliders = new List<EnemyLeg>();
        }

        /// <summary>
        /// Add enemy leg to list/
        /// </summary>
        public void AddNewLegs(EnemyLeg left, EnemyLeg right)
        {
            _leftColliders.Add(left);
            _rightColliders.Add(right);
        }

        /// <summary>
        /// Change friction to all legs.
        /// Uses fot animate left leg.
        /// </summary>
        public void SetLeftFriction()
        {
            for (int i = 0; i < _leftColliders.Count; i++)
            {
                _leftColliders[i].ColToChangeMaterial.material = normalFriction;
            }

            for (int i = 0; i < _rightColliders.Count; i++)
            {
                _rightColliders[i].ColToChangeMaterial.material = zeroFriction;
            }
        }

        /// <summary>
        /// Change friction to all legs.
        /// Uses fot animate right leg.
        /// </summary>
        public void SetRightFriction()
        {
            for (int i = 0; i < _leftColliders.Count; i++)
            {
                _leftColliders[i].ColToChangeMaterial.material = zeroFriction;
            }

            for (int i = 0; i < _rightColliders.Count; i++)
            {
                _rightColliders[i].ColToChangeMaterial.material = normalFriction;
            }
        }
    }
}