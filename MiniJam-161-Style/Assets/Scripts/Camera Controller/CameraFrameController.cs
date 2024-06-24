using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Camera_Controller
{
    public class CameraFrameController : MonoBehaviour
    {
        public GameObject photographyCamera;

        [Header("Camera Frame Object")]
        public GameObject defaultFrameObject;
        public GameObject aimingFrameObject;

        [Header("Camera Frame Settings")]
        public float changeScaleDuration = 0.1f;
        [Range(0,1)] public float followSpeed = 0.05f;

        private RectTransform _aimingFrameRectTransform;
        private Image _image;

        private void Start()
        {
            _aimingFrameRectTransform = aimingFrameObject.GetComponent<RectTransform>();
        }

        public void ChangeToAimingFrameObject()
        {
            defaultFrameObject.SetActive(false);
            aimingFrameObject.SetActive(true);
        }

        public void ChangeToDefaultFrameObject()
        {
            defaultFrameObject.SetActive(true);
            UpdateSize(Vector2.zero);
        }

        public void UpdateSize(Vector2 sizeDelta)
        {
            _aimingFrameRectTransform.DOSizeDelta(sizeDelta, changeScaleDuration).SetUpdate(true);
        }

        private void Update()
        {
            // follow actual camera
            transform.position += (photographyCamera.transform.position - transform.position) * followSpeed;
        }
    }
}
