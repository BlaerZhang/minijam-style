using System.Collections;
using Globals;
using UnityEngine;

namespace UI_Utils
{
    public class AutoClose : MonoBehaviour
    {
        public float timeToInspect = 2f;
        private void OnEnable()
        {
            StartCoroutine(AutoCloseInspector());
        }

        private IEnumerator AutoCloseInspector()
        {
            yield return new WaitForSecondsRealtime(timeToInspect);
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);

            // print($"{name} closed");
        }
    }
}