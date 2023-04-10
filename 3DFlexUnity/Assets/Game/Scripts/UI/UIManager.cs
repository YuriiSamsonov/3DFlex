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
        private SpawnManager spawnManager;
        
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
        private GameObject crosshair;
        
        [field: SerializeField] 
        private GameObject damageScreen;

        [field: SerializeField] 
        private float waveNumberScreenHideDelay = 4f;

        private ScoreUI _scoreUI;
        private WaveNumberUI _waveNumberUI;

        private bool _paused;
        private bool _playerDead;

        private void Start()
        {
            waveCountUI.SetActive(true);
            StartCoroutine(HideWaveCountAfterDelay(waveNumberScreenHideDelay));
            _scoreUI = scoreUI.GetComponent<ScoreUI>();
            _waveNumberUI = waveCountUI.GetComponent<WaveNumberUI>();
            playerMono.OnPlayerDiedEvent += OnPlayerDeadEventHandler;
            spawnManager.OnSpawnNewWave += OnSpawnNewWaveEventHandler;
        }

        private void OnPlayerDeadEventHandler(EventArgs _)
        {
            Time.timeScale = 0f;
            CursorOnPause();
            OnPause(true, false);
            deathScreen.SetActive(true);
            damageScreen.SetActive(false);
            _playerDead = true;
        }

        private void OnSpawnNewWaveEventHandler(EventArgs _)
        {
            waveCountUI.SetActive(true);
            _waveNumberUI.UpdateWaveCount();
            StartCoroutine(HideWaveCountAfterDelay(waveNumberScreenHideDelay));
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
            crosshair.SetActive(hudState);
            exitButton.SetActive(uiState);
            scoreUI.SetActive(uiState);
            _scoreUI.UpdateScore();
            pauseScreen.SetActive(uiState);
            waveCountUI.SetActive(uiState);
            _waveNumberUI.UpdateWaveCount();
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
        
        private IEnumerator HideWaveCountAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            waveCountUI.SetActive(false);
        }
    }
}