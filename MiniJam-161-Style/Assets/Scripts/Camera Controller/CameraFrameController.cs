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
        private GameObject _aimingCrosshair;

        [Header("Camera Frame Settings")]
        public float changeScaleDuration = 0.1f;
        [Range(0,1)] public float followSpeed = 0.05f;

        private RectTransform _aimingFrameRectTransform;
        private Image _image;

        private void Start()
        {
            _aimingFrameRectTransform = aimingFrameObject.GetComponent<RectTransform>();
            _aimingCrosshair = aimingFrameObject.transform.GetChild(0).gameObject;
        }

        public void ChangeToAimingFrameObject()
        {
            defaultFrameObject.SetActive(false);
            aimingFrameObject.SetActive(true);
            _aimingCrosshair.SetActive(true);
        }

        public void ChangeToDefaultFrameObject()
        {
            defaultFrameObject.SetActive(true);
            _aimingCrosshair.SetActive(false);
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
