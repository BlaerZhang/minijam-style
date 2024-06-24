using System;
using UnityEngine;

namespace UIFunction
{
    public class PhotoInspector : MonoBehaviour
    {
        private void OnMouseDown()
        {
            CloseInspector();
        }

        private void CloseInspector()
        {
            gameObject.SetActive(false);
        }

        public void SavePhoto()
        {

        }
    }
}