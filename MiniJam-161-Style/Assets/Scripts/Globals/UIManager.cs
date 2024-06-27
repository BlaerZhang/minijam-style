using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Globals
{
    public class UIManager : MonoBehaviour
    {
        [Header("Menu UI")]
        public GameObject photoInspectorContent;

        [Header("Resources UI")]
        public Slider batteryUI;

        [Header("Level Objective UI")]
        public TextMeshProUGUI currentObjectiveUI;

        public void UpdateLevelObjectiveUI()
        {
            // TODO: objective text formatting
            // currentObjectiveUI.text =
        }

        public void UpdateCameraBatteryPercentage(float batteryPercentage)
        {
            batteryUI.value = batteryPercentage;
        }

        public void ShowPhotoInspector(bool show = true)
        {
            print("ShowPhotoInspector");
            photoInspectorContent.SetActive(show);
        }
    }
}