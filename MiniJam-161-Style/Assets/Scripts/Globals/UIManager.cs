using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Globals
{
    public class UIManager : MonoBehaviour
    {
        [Header("Menu UI")]
        public GameObject gameOverMenu;

        // TODO: 1. click to continue, 2. button to save photo to desktop
        public GameObject photoUI;

        [Header("Resources UI")]
        // public TextMeshProUGUI filmCountUI;
        public Slider batteryUI;

        [Header("Level Objective UI")]
        public TextMeshProUGUI currentObjectiveUI;

        // public void UpdateFilmCount(int filmCount)
        // {
        //     filmCountUI.text = "Film Count: " + filmCount;
        // }

        public void UpdateCameraBatteryPercentage(float batteryPercentage)
        {
            batteryUI.value = batteryPercentage;
        }

        /// <summary>
        /// show after shooting a photo
        /// </summary>
        public void ShowPhotoUI(bool show)
        {
            if (photoUI.activeSelf)
            {
                // pause the game
                GameManager.Instance.PauseGame();
            }
            else
            {
                // continue the game
                GameManager.Instance.ResumeGame();

                photoUI.SetActive(true);
            }
        }

        public void ShowGameOverMenu(bool show)
        {
            gameOverMenu.SetActive(show);
        }

        public void OnRestartButtonPressed()
        {
            // Implement restart logic, e.g., reload the scene
            Time.timeScale = 1f;
        }

        public void OnQuitButtonPressed()
        {
            // Implement quit logic
            Application.Quit();
        }
    }
}