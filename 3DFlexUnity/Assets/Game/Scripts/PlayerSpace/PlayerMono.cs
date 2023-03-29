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

        private void Start()
        {
            _currentHp = maxHp;
        }

        public void OnHit()
        {
            const int damageTaken = 1;
            if (!_justHit)
            {
                _justHit = true;
                _currentHp -= damageTaken;
                damageSound.Play();
                OnPlayerHitEvent(EventArgs.Empty);
                StartCoroutine(CoolDownHitWithSeconds());
            }
        }

        public void OnHeal(int heal)
        {
            _currentHp += heal;
        }

        private IEnumerator CoolDownHitWithSeconds()
        {
            yield return new WaitForSeconds(0.5f);
            _justHit = false;
        }
    }
}