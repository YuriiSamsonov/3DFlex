using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class MoveCamera : MonoBehaviour
    {
        /// <summary>
        /// Player transform.
        /// </summary>
        [field: SerializeField, Tooltip("Player transform.")] 
        private Transform player;

        private void Update()
        {
            transform.position = player.transform.position;
        }
    }
}