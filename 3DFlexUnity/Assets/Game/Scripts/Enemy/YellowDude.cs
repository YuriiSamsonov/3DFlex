using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class YellowDude : MonoBehaviour
    {
        public Transform[] targets;

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