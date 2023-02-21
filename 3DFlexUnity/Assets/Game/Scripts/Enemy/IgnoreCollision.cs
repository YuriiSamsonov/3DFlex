using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class IgnoreCollision : MonoBehaviour
    {
        [field: SerializeField] 
        private Collider[] allColliders;

        private void Awake()
        {
            for (int i = 0; i < allColliders.Length; i++)
            {
                for (int j = 0; j < allColliders.Length; j++)
                {
                    Physics.IgnoreCollision(allColliders[i], allColliders[j], true);
                }
            }
        }
    }
}