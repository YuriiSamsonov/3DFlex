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

        private void Update()
        {
            text.text = "HP : " + playerMono.CurrentHp;
            playerMono.OnPlayerHitEvent += OnPlayerDamaged;
        }

        private void OnPlayerDamaged(EventArgs _)
        {
            damageScreen.SetActive(true);
            StartCoroutine(HideDamageScreen());
        }

        private IEnumerator HideDamageScreen()
        {
            yield return new WaitForSecondsRealtime(0.25f);
            damageScreen.SetActive(false);
        }
    }
}