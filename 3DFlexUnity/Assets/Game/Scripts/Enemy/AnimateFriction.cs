using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class AnimateFriction : MonoBehaviour
    {
        [field: SerializeField] 
        private PhysicMaterial normalFriction;
        
        [field: SerializeField] 
        private PhysicMaterial zeroFriction;
        
        [field: SerializeField] 
        private Collider[] leftCollider;
        
        [field: SerializeField] 
        private Collider[] rightCollider;

        public void SetLeftFriction()
        {
            for (int i = 0; i < leftCollider.Length; i++)
            {
                leftCollider[i].material = normalFriction;
            }

            for (int i = 0; i < rightCollider.Length; i++)
            {
                rightCollider[i].material = zeroFriction;
            }

        }

        public void SetRightFriction()
        {
            for (int i = 0; i < leftCollider.Length; i++)
            {
                leftCollider[i].material = zeroFriction;
            }

            for (int i = 0; i < rightCollider.Length; i++)
            {
                rightCollider[i].material = normalFriction;
            }
        }
    }
}