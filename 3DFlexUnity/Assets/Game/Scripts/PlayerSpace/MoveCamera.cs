using System;
using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class MoveCamera : MonoBehaviour
    {
        [field: SerializeField] 
        private Transform player;

        private void Update()
        {
            transform.position = player.transform.position;
        }
    }
}