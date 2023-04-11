using System;
using System.Collections;
using Game.Scripts.Enemy;
using Game.Scripts.PlayerSpace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        /// Enemy spawn manager.
        /// </summary>
        [field: SerializeField, Header("Scene References"), Tooltip("Enemy spawn manager.")] 
        private SpawnManager spawnManager;
        
        /// <summary>
        /// Player in scene.
        /// </summary>
        [field: SerializeField, Tooltip("Player in scene.")] 
        private PlayerMono playerMono;
        
        /// <summary>
        /// UI showing wave count.
        /// </summary>
        [field: SerializeField, Header("On Pause Interface"), Tooltip("UI showing wave count.")]
        private GameObject waveCountUI;
        
        /// <summary>
        /// UI showing player score.
        /// </summary>
        [field: SerializeField, Tooltip("UI showing player score.")] 
        private GameObject scoreUI;
        
        /// <summary>
        /// Exit button in pause menu.
        /// Loads MainMenu scene on pressed.
        /// </summary>
        [field: SerializeField, Tooltip("Exit button in pause menu.")] 
        private GameObject exitButton;
        
        /// <summary>
        /// UI showing player health.
        /// </summary>
        [field: SerializeField, Header("On Play Interface"), Tooltip("")] 
        private GameObject hpBar;
        
        /// <summary>
        /// Point in the middle of the screen.
        /// </summary>
        [field: SerializeField, Tooltip("Point in the middle of the screen.")] 
        private GameObject crosshair;
        
        /// <summary>
        /// Pause screen.
        /// Shows on pause.
        /// </summary>
        [field: SerializeField, Header("Screens"), Tooltip("")] 
        private GameObject pauseScreen;

        /// <summary>
        /// Death screen.
        /// Shows on death.
        /// </summary>
        [field: SerializeField] 
        private GameObject deathScreen;
        
        /// <summary>
        /// Damage screen.
        /// Shows on damage taking.
        /// </summary>
        [field: SerializeField] 
        private GameObject damageScreen;

        private const float WaveNumberScreenHideDelay = 4f;

        private ScoreUI _scoreUI;
        private WaveNumberUI _waveNumberUI;

        private bool _isPaused;
        private bool _isPlayerDead;

        private void Start()
        {
            waveCountUI.SetActive(true);
            StartCoroutine(HideWaveCountAfterDelay(WaveNumberScreenHideDelay));
            _scoreUI = scoreUI.GetComponent<ScoreUI>();
            _waveNumberUI = waveCountUI.GetComponent<WaveNumberUI>();
            playerMono.OnPlayerDiedEvent += OnPlayerDeadEventHandler;
            spawnManager.OnSpawnNewWave += OnSpawnNewWaveEventHandler;
        }

        /// <summary>
        /// OnPlayerDeathCallback stops time and shows death menu.
        /// </summary>
        /// <param name="_"></param>
        private void OnPlayerDeadEventHandler(EventArgs _)
        {
            Time.timeScale = 0f;
            CursorOnPause();
            OnPause(true, false);
            deathScreen.SetActive(true);
            damageScreen.SetActive(false);
            _isPlayerDead = true;
        }

        /// <summary>
        /// Shows wave UI for the short time.
        /// </summary>
        /// <param name="_"></param>
        private void OnSpawnNewWaveEventHandler(EventArgs _)
        {
            waveCountUI.SetActive(true);
            _waveNumberUI.UpdateWaveCount();
            StartCoroutine(HideWaveCountAfterDelay(WaveNumberScreenHideDelay));
        }

        /// <summary>
        /// Shows pause menu depends on whether the menu is currently open.
        /// </summary>
        /// <param name="context"></param>
        public void OnEscapeButton(InputAction.CallbackContext context)
        {
            if (!_isPaused && !_isPlayerDead)
            {
                Time.timeScale = 0f;
                CursorOnPause();
                OnPause(true, false);
            }
            else if (_isPaused)
            {
                Time.timeScale = 1f;
                SetCursorOnPlay();
                OnPause(false, true);
            }
        }

        /// <summary>
        /// Change active HUD state depends on application paused or not.
        /// </summary>
        /// <param name="uiState"></param>
        /// <param name="hudState"></param>
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
            _isPaused = uiState;
        }

        /// <summary>
        /// Set cursor locked and invisible.
        /// </summary>
        private void SetCursorOnPlay()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        /// <summary>
        /// Set cursor unlocked and visible.
        /// </summary>
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