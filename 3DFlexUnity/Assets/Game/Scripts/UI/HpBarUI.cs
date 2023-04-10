using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.PlayerSpace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class HpBarUI : MonoBehaviour
    {
        [field: SerializeField] 
        private PlayerMono playerMono;
        
        [field: SerializeField] 
        private Text text;
        
        [field: SerializeField] 
        private GameObject damageScreen;

        private float _screenClock;

        private bool _isScreenShown;

        private void Start()
        {
            playerMono.OnPlayerHitEvent += OnPlayerDamaged;
            UpdateHpBar();
        }

        private void OnPlayerDamaged(EventArgs _)
        {
            UpdateHpBar();
            damageScreen.SetActive(true);
            _screenClock += .25f;
            if (!_isScreenShown)
                StartCoroutine(HideDamageScreen());
        }

        private void UpdateHpBar()
        {
            text.text = "HP : " + playerMono.CurrentHp;
        }

        private IEnumerator HideDamageScreen() // 47:40
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