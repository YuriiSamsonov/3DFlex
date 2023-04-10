using System;
using System.Collections;
using Game.Scripts.Utils;
using UnityEngine;

namespace Game.Scripts.PlayerSpace
{
    public class PlayerMono : MonoBehaviour
    {
        [field: SerializeField] 
        private int maxHp;

        [field: SerializeField] 
        private AudioSource damageSound;

        private int _currentHp;
        public int CurrentHp => _currentHp;
        
        private bool _justHit;

        public event Event<EventArgs> OnPlayerHitEvent;
        public event Event<EventArgs> OnPlayerDiedEvent;

        private void Start()
        {
            _currentHp = maxHp;
        }

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
                {
                    OnPlayerDiedEvent(EventArgs.Empty);
                }
            }
        }

        private IEnumerator CoolDownHitWithSeconds()
        {
            yield return Variables.WaitForHalfASecond; 
            _justHit = false;
        }
    }
}