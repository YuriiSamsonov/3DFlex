using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemy;
using Game.Scripts.PlayerSpace;
using UnityEngine;
using UnityEngine.InputSystem;
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
        private bool _playerDead;

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
            
            if (playerMono.CurrentHp <= 0)
            {
                Time.timeScale = 0f;
                CursorOnPause();
                OnPause(true, false);
                deathScreen.SetActive(true);
                damageScreen.SetActive(false);
                _playerDead = true;
            }
        }

        public void OnEscapeButton(InputAction.CallbackContext context)
        {
            if (!_paused && !_playerDead)
            {
                Time.timeScale = 0f;
                CursorOnPause();
                OnPause(true, false);
            }
            else if (_paused)
            {
                Time.timeScale = 1f;
                SetCursorOnPlay();
                OnPause(false, true);
            }
        }

        private void OnPause(bool uiState, bool hudState)
        {
            hpBar.SetActive(hudState);
            point.SetActive(hudState);
            exitButton.SetActive(uiState);
            scoreUI.SetActive(uiState);
            pauseScreen.SetActive(uiState);
            waveCountUI.SetActive(uiState);
            _paused = uiState;
        }

        private void SetCursorOnPlay()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void CursorOnPause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        private IEnumerator HideWaveCountWithFourSeconds()
        {
            yield return new WaitForSecondsRealtime(4);
            waveCountUI.SetActive(false);
        }
    }
}