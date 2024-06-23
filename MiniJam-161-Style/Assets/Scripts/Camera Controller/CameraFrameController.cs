using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Camera_Controller
{
    public class CameraFrameController : MonoBehaviour
    {
        public GameObject photographyCamera;

        [Header("Camera Frame Settings")]
        public float changeScaleDuration = 0.1f;
        [Range(0,1)] public float followSpeed = 0.05f;

        private RectTransform _rectTransform;
        private Image _image;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _image = GetComponent<Image>();
        }

        public void UpdateFrameImage(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void UpdateSize(Vector2 sizeDelta)
        {
            _rectTransform.DOSizeDelta(sizeDelta, changeScaleDuration);
        }

        private void Update()
        {
            // follow actual camera
            transform.position += (photographyCamera.transform.position - transform.position) * followSpeed;
        }
    }
}
