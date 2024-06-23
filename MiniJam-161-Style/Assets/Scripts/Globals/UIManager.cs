using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Globals
{
    public class UIManager : MonoBehaviour
    {
        [Header("Menu UI")]
        public GameObject levelCompletedMenu;
        public GameObject levelFailedMenu;

        // TODO: 2. button to save photo to desktop
        public GameObject photoUI;

        [Header("Resources UI")]
        public Slider batteryUI;

        [Header("Level Objective UI")]
        public TextMeshProUGUI currentObjectiveUI;

        public void ResetGameSceneUI()
        {
            levelCompletedMenu.SetActive(false);
            levelFailedMenu.SetActive(false);
        }

        public void UpdateLevelObjectiveUI()
        {
            // TODO: objective text formatting
            // currentObjectiveUI.text =
        }

        public void UpdateCameraBatteryPercentage(float batteryPercentage)
        {
            batteryUI.value = batteryPercentage;
        }

        public void ShowLevelCompletedMenu()
        {
            levelCompletedMenu.SetActive(true);
        }

        public void ShowLevelFailedMenu()
        {
            levelFailedMenu.SetActive(true);
        }

        // TODO: show photo
        /// <summary>
        /// show after shooting a photo
        /// </summary>
        public void ShowPhotoUI(bool show)
        {
            if (photoUI.activeSelf)
            {

            }
            else
            {
                photoUI.SetActive(true);
            }
        }
    }
}