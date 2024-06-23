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

        private void Update()
        {
            // resume game after left-clicking in photo inspecting menu
            if (currentState.Equals(GameState.LevelCompleted) || currentState.Equals(GameState.LevelFailed))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ChangeState(GameState.Playing);
                }
            }
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
                    Time.timeScale = 1f;
                    uiManager.ResetGameSceneUI();
                    break;
                case GameState.LevelCompleted:
                    Time.timeScale = 0f;
                    uiManager.ShowLevelCompletedMenu();
                    levelManager.NextLevel();
                    break;
                case GameState.LevelFailed:
                    Time.timeScale = 0f;
                    uiManager.ShowLevelFailedMenu();
                    break;
            }
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
