using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class AnimationTarget : MonoBehaviour
    {
        /// <summary>
        /// All parts containing PhysicalBodyPart.cs
        /// </summary>
        [field: SerializeField, Tooltip("All parts containing PhysicalBodyPart.cs")]
        private Transform[] targets;

        /// <summary>
        /// Apply target body part to the enemy body part.
        /// </summary>
        /// <param name="enemy"></param>
        public void ApplyTargets(Enemy enemy)
        {
            var parts = enemy.BodyParts;
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Init(targets[i]);
            }
        }
    }
}