using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enemy.EnemyBodySpace;
using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class AnimateFriction : MonoBehaviour
    {
        [field: SerializeField] 
        private PhysicMaterial normalFriction;
        
        [field: SerializeField] 
        private PhysicMaterial zeroFriction;

        private List<EnemyLeg> _leftColliders;
        private List<EnemyLeg> _rightColliders;
        
        public void SetCollidersFriction()
        {
            _leftColliders = new List<EnemyLeg>();
            _rightColliders = new List<EnemyLeg>();
            
            FindAllColliders();
        }
        
        private void FindAllColliders()
        {
            var list = FindObjectsOfType<EnemyLeg>().ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].RightLeg)
                {
                    _rightColliders.Add(list[i]);
                }

                if (!list[i].RightLeg)
                {
                    _leftColliders.Add(list[i]);
                }
            }
        }

        public void SetLeftFriction()
        {
            for (int i = 0; i < _leftColliders.Count; i++)
            {
                //leftCollider[i].material = normalFriction;
                _leftColliders[i].ColToChangeMaterial.material = normalFriction;
            }

            for (int i = 0; i < _rightColliders.Count; i++)
            {
                //rightCollider[i].material = zeroFriction;
                _rightColliders[i].ColToChangeMaterial.material = zeroFriction;
            }
        }

        public void SetRightFriction()
        {
            for (int i = 0; i < _leftColliders.Count; i++)
            {
                //leftCollider[i].material = zeroFriction;
                _leftColliders[i].ColToChangeMaterial.material = zeroFriction;
            }

            for (int i = 0; i < _rightColliders.Count; i++)
            {
                //rightCollider[i].material = normalFriction;
                _rightColliders[i].ColToChangeMaterial.material = normalFriction;
            }
        }
    }
}