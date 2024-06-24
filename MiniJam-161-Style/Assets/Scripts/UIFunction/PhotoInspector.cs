using System;
using System.Collections;
using Globals;
using UnityEngine;

namespace UIFunction
{
    public class PhotoInspector : MonoBehaviour
    {
        public float timeToInspect = 2f;

        private void OnEnable()
        {
            StartCoroutine(CloseInspector());
        }

        private IEnumerator CloseInspector()
        {
            yield return new WaitForSecondsRealtime(timeToInspect);
            GameManager.Instance.ChangeState(GameManager.GameState.Playing);

            // TODO: indicate save photo button
        }
    }
}