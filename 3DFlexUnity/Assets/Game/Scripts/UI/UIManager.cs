using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemy;
using Game.Scripts.PlayerSpace;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [field: SerializeField] 
        private SpawnManger spawnManger;
        
        [field: SerializeField] 
        private PlayerMono playerMono;
        
        [field: SerializeField] 
        private GameObject waveCountUI;
        
        [field: SerializeField] 
        private GameObject scoreUI;
        
        [field: SerializeField] 
        private GameObject deathScreen;
        
        [field: SerializeField] 
        private GameObject hpBar;
        
        [field: SerializeField] 
        private GameObject exitButton;
        
        [field: SerializeField] 
        private GameObject pauseScreen;
        
        [field: SerializeField] 
        private GameObject point;
        
        [field: SerializeField] 
        private GameObject damageScreen;

        private bool _paused;

        private void Start()
        {
            waveCountUI.SetActive(true);
            StartCoroutine(HideWaveCountWithFourSeconds());
        }

        private void Update()
        {
            if (spawnManger.AliveEnemies <= 0)
            {
                waveCountUI.SetActive(true);
                StartCoroutine(HideWaveCountWithFourSeconds());
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !_paused)
            {
                Time.timeScale = 0f;
                _paused = true;
                hpBar.SetActive(false);
                exitButton.SetActive(true);
                scoreUI.SetActive(true);
                pauseScreen.SetActive(true);
                point.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && _paused)
            {
                Time.timeScale = 1f;
                _paused = false;
                hpBar.SetActive(true);
                exitButton.SetActive(false);
                scoreUI.SetActive(false);
                pauseScreen.SetActive(false);
                point.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (playerMono.CurrentHp <= 0)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                deathScreen.SetActive(true);
                scoreUI.SetActive(true);
                exitButton.SetActive(true);
                hpBar.SetActive(false);
                point.SetActive(false);
                damageScreen.SetActive(false);
            }
        }

        private IEnumerator HideWaveCountWithFourSeconds()
        {
            yield return new WaitForSecondsRealtime(4);
            waveCountUI.SetActive(false);
        }
    }
}