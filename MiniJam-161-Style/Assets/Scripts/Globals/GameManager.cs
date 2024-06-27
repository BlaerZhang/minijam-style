using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Globals
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [HideInInspector] public AudioManager audioManager;
        [HideInInspector] public LevelManager levelManager;
        [HideInInspector] public UIManager uiManager;

        public enum GameState
        {
            MainMenu,
            Playing,
            LevelCompleted,
            LevelFailed,
        }

        public GameState currentState;

        // TODO: block player movement
        public bool allowInput = true;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }

            audioManager = GetComponentInChildren<AudioManager>();
            levelManager = GetComponentInChildren<LevelManager>();
            uiManager = GetComponentInChildren<UIManager>();
        }

        void Start()
        {
            ChangeState(GameState.MainMenu);
        }

        public void ChangeState(GameState newState)
        {
            currentState = newState;
            OnStateChanged(newState);
        }

        private void OnStateChanged(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    // SceneManager.LoadScene("MainMenu");
                    break;
                case GameState.Playing:
                    ResumeGame();
                    break;
                case GameState.LevelCompleted:
                    PauseGame();
                    uiManager.ShowPhotoInspector();
                    // levelManager.NextLevel();
                    break;
                case GameState.LevelFailed:
                    PauseGame();
                    uiManager.ShowPhotoInspector();
                    break;
            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            allowInput = true;
            uiManager.ShowPhotoInspector(false);
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            allowInput = false;
        }

        /// <summary>
        /// called when button clicked
        /// </summary>
        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
