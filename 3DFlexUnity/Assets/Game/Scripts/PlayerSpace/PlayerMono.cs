using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class PlayerMono : MonoBehaviour
    {
        /// <summary>
        /// Max health point of the player.
        /// </summary>
        [field: SerializeField, Min(1), Tooltip("Max health point of the player.")] 
        private int maxHp;

        /// <summary>
        /// Sound that plays when player takes damage.
        /// </summary>
        [field: SerializeField, Tooltip("Sound that plays when player takes damage.")] 
        private AudioSource damageSound;

        private int _currentHp;
        /// <summary>
        /// Health points of the player when application in runtime.
        /// Changes when player take damage or healing.
        /// </summary>
        public int CurrentHp => _currentHp;
        
        private bool _wasHitThisFrame;

        /// <summary>
        /// Informs when player takes damage.
        /// </summary>
        public event Event<EventArgs> OnPlayerHitEvent;
        
        /// <summary>
        /// Informs when player dies.
        /// </summary>
        public event Event<EventArgs> OnPlayerDiedEvent;

        private void Start()
        {
            _currentHp = maxHp;
        }

        /// <summary>
        /// Deals damage to the player.
        /// If the player has just taken damage provides the player with immunity frames.
        /// </summary>
        public void OnPlayerTakeDamage()
        {
            const int damageTaken = 1;
            if (!_wasHitThisFrame)
            {
                _wasHitThisFrame = true;
                _currentHp -= damageTaken;
                damageSound.Play();
                OnPlayerHitEvent(EventArgs.Empty);
                StartCoroutine(CoolDownHitWithSeconds());
                
                if (_currentHp <= 0)
                    OnPlayerDiedEvent(EventArgs.Empty);
            }
        }

        private IEnumerator CoolDownHitWithSeconds()
        {
            yield return Variables.WaitForHalfASecond; 
            _wasHitThisFrame = false;
        }
    }
}