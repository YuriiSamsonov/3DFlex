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
        
        private bool _justHit;

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
        /// If player just taken damage stops ability to take damage and starts a countdown
        /// to the next ability to take damage.
        /// </summary>
        public void OnPlayerTakeDamage()
        {
            const int damageTaken = 1;
            if (!_justHit)
            {
                _justHit = true;
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
            _justHit = false;
        }
    }
}