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
        public TaskList taskList;

        public void UpdateLevelObjectiveUI()
        {
            // TODO: check the box

            // then show the
            taskList.UpdateTask();
        }

        public void UpdateCameraBatteryPercentage(float batteryPercentage)
        {
            batteryUI.value = batteryPercentage;
        }

        public void ShowPhotoInspector(bool result, bool show = true)
        {
            // print("ShowPhotoInspector");
            photoInspectorContent.SetActive(show);
            photoInspectorContent.transform.Find("Photo Result").GetComponent<TMP_Text>().text =
                result ? "Nice One!" : "Try Again";
        }
    }
}