using System;
using System.Collections;
using System.IO;
using Camera_Controller;
using Globals;
using UnityEngine;
using UnityEngine.UI;

namespace UIFunction
{
    public class PhotoInspector : MonoBehaviour
    {
        [Header("Photo Display Settings")]
        public Camera screenshotCamera;
        public Image displayImage;

        [Header("Photo Save Settings")]
        public string photoFileName = "Photo";
        public string photoFolderName = "Photoooooooooooos";

        private Texture2D _renderedTexture;

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     StartCoroutine(DisplayPhoto());
            // }

            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                SavePhoto();
            }
        }

        private void OnEnable()
        {
            CameraController.OnPhotoCaptured += OnPhotoCaptured;
        }

        private void OnDisable()
        {
            CameraController.OnPhotoCaptured -= OnPhotoCaptured;
        }

        private void OnPhotoCaptured(Rect captureRect)
        {
            StartCoroutine(DisplayPhoto(captureRect));
        }

        IEnumerator DisplayPhoto(Rect captureRect)
        {
            yield return new WaitForEndOfFrame();

            // screen texture without selected layer
            RenderTexture screenTexture = new RenderTexture(Screen.width, Screen.height, 16);
            screenshotCamera.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            screenshotCamera.Render();

            // cut from the screen texture
            _renderedTexture = new Texture2D((int)captureRect.width, (int)captureRect.height, TextureFormat.RGB24, false);

            _renderedTexture.ReadPixels(captureRect, 0, 0);
            RenderTexture.active = null;
            _renderedTexture.Apply();

            // convert the texture to sprite
            Sprite screenshotSprite = Sprite.Create(_renderedTexture, new Rect(0, 0, _renderedTexture.width, _renderedTexture.height), new Vector2(0.5f, 0.5f));

            displayImage.sprite = screenshotSprite;

            yield return new WaitForEndOfFrame();
            print("photo captured");
        }

        private void SavePhoto()
        {
            if (_renderedTexture == null)
            {
                print("No photo is available");
                return;
            }

            string fileName = $"{photoFileName}_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            string folderName = $"{photoFolderName}_";
            string folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Debug.Log("Created folder at: " + folderPath);
            }
            else
            {
                Debug.Log("Folder already exists at: " + folderPath);
            }

            string filePath = Path.Combine(folderPath, fileName);

            byte[] bytes = _renderedTexture.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Screenshot saved to: " + filePath);
        }
    }
}