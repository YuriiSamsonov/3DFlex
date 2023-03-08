using System;
using Game.Scripts.PlayerSpace;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class Recovery : MonoBehaviour
    {
        [field: SerializeField] 
        private PlayerMono playerMono;
        
        [field: SerializeField] 
        private int recoveryPoints;

        private int _healPoints = 1;

        private void Update()
        {
            transform.Rotate(0,1,0, Space.Self);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerMono.OnHeal(_healPoints);
                Debug.Log("playerHealed");
                Destroy(gameObject);
            }
        }
    }
}