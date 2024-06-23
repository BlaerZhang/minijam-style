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
            Paused,
            GameOver
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

        private void ChangeState(GameState newState)
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
                    // SceneManager.LoadScene("GameScene");
                    break;
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                case GameState.GameOver:
                    uiManager.ShowGameOverMenu(true);
                    break;
            }
        }

        public void StartGame()
        {
            ChangeState(GameState.Playing);
            Time.timeScale = 1f;
        }

        public void PauseGame()
        {
            ChangeState(GameState.Paused);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }

        public void GameOver()
        {
            ChangeState(GameState.GameOver);
        }

        public void ReturnToMainMenu()
        {
            ChangeState(GameState.MainMenu);
        }
    }
}
