using System;
using System.Collections; 
using Game.Scripts.PlayerSpace;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class HpBarUI : MonoBehaviour
    {
        /// <summary>
        /// Player in the scene.
        /// </summary>
        [field: SerializeField, Tooltip("Player in the scene.")] 
        private PlayerMono playerMono;
        
        /// <summary>
        /// Health bar text.
        /// </summary>
        [field: SerializeField, Tooltip("Health bar text.")] 
        private Text hpBarText;
        
        /// <summary>
        /// Red screen appears when player takes damage.
        /// </summary>
        [field: SerializeField, Tooltip("Red screen appears when player takes damage.")] 
        private GameObject damageScreen;

        private float _screenClock;

        private bool _isScreenShown;

        private void Start()
        {
            playerMono.OnPlayerHitEvent += OnPlayerDamaged;
            UpdateHpBar();
        }

        /// <summary>
        /// Shows damage screen and update health bar when player takes damage.
        /// </summary>
        /// <param name="_"></param>
        private void OnPlayerDamaged(EventArgs _)
        {
            UpdateHpBar();
            damageScreen.SetActive(true);
            _screenClock += .25f;
            if (!_isScreenShown)
                StartCoroutine(HideDamageScreen());
        }

        /// <summary>
        /// Set new text value on the hp bar.
        /// </summary>
        private void UpdateHpBar()
        {
            hpBarText.text = "HP : " + playerMono.CurrentHp;
        }

        /// <summary>
        /// Hides damageScreen if all timers stops.
        /// </summary>
        /// <returns></returns>
        private IEnumerator HideDamageScreen()
        {
            var currentTime = 0.0f;
            while (currentTime < _screenClock)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            damageScreen.SetActive(false);
        }
    }
}