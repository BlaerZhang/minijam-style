using System;
using System.Collections.Generic;
using Globals;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Camera_Controller
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Components")]
        public CameraFrameController cameraFrameController;
        public CameraBattery cameraBattery;

        [Header("Camera Frame Image")]
        public Sprite defaultFrameImage;
        public Sprite aimingFrameImage;

        [Header("Camera Settings")]
        public float zoomDelta = 100f;
        public Vector2 cameraXLimits;
        public Vector2 defaultCameraFrameSize = new Vector2(300, 200);

        public static Action OnBatteryDead;

        private RectTransform _rectTransform;
        private bool _isAiming = false;
        private Vector2 _currentFrameSize;

        private void OnEnable()
        {
            OnBatteryDead += StopAiming;
        }

        private void OnDisable()
        {
            OnBatteryDead -= StopAiming;
        }

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        void Update()
        {
            // follow mouse
            transform.position = Input.mousePosition;

            // aim
            if (Input.GetMouseButtonDown(1))
            {
                StartAiming();
            }

            // aiming
            if (_isAiming)
            {
                cameraBattery.ReduceBatteryPercentage();
            }
            else
            {
                cameraBattery.RecoverBatteryPercentage();
            }

            // exit aiming
            if (Input.GetMouseButtonUp(1))
            {
                StopAiming();
            }

            // shoot
            if (_isAiming && Input.GetMouseButtonDown(0))
            {
                Shoot();
                StopAiming();
            }

            // zoom
            ZoomInAndOut();
        }

        private void StartAiming()
        {
            _isAiming = true;
            cameraFrameController.UpdateFrameImage(aimingFrameImage);

            // TODO: gradually open

            _currentFrameSize = defaultCameraFrameSize;
            _rectTransform.sizeDelta = defaultCameraFrameSize;
            cameraFrameController.UpdateSize(_currentFrameSize);
        }

        private void StopAiming()
        {
            _isAiming = false;
            cameraFrameController.UpdateFrameImage(defaultFrameImage);

            _rectTransform.sizeDelta = Vector2.zero;
            cameraFrameController.UpdateSize(Vector2.zero);
        }

        private void ZoomInAndOut()
        {
            if (_isAiming)
            {
                if (Mathf.Abs(Input.mouseScrollDelta.y) >= 1)
                {
                    print("zoom");
                    float currentFrameSizeX = _currentFrameSize.x + zoomDelta * Input.mouseScrollDelta.y;
                    currentFrameSizeX = Mathf.Clamp(currentFrameSizeX, cameraXLimits.x, cameraXLimits.y);

                    float currentFrameSizeY = currentFrameSizeX * 2 / 3;
                    // currentFrameSizeY = Mathf.Clamp(currentFrameSizeY, 0, 1080);

                    _currentFrameSize = new Vector2(currentFrameSizeX, currentFrameSizeY);
                    _rectTransform.sizeDelta = _currentFrameSize;
                    cameraFrameController.UpdateSize(_currentFrameSize);
                }
            }
        }

        // TODO: bullet time
        private void StartBulletTime()
        {

        }

        private void StopBulletTime()
        {

        }

        private void Shoot()
        {
            print("Shoot");
            //Todo: check resource, then -1

            Vector3[] corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            // 这些角点是屏幕空间的坐标，对于Overlay模式，它们相当于屏幕坐标
            Vector3 screenBottomLeft = corners[0];
            Vector3 screenTopRight = corners[2];

            // 将屏幕坐标转换为世界坐标
            Vector3 worldBottomLeft = Camera.main.ScreenToWorldPoint(screenBottomLeft);
            Vector3 worldTopRight = Camera.main.ScreenToWorldPoint(screenTopRight);

            // 计算中心点和大小
            Vector3 center = (worldBottomLeft + worldTopRight) / 2;
            Vector2 size = new Vector2(Mathf.Abs(worldTopRight.x - worldBottomLeft.x), Mathf.Abs(worldTopRight.y - worldBottomLeft.y));

            // Raycast
            Collider2D[] overlapBoxHits = Physics2D.OverlapBoxAll(center, size, 0, LayerMask.GetMask("Shootable"));

            // check if fully inside
            List<string> fullyInsideTags = new List<string>();
            List<string> partlyInsideTags = new List<string>();

            foreach (var collider2D in overlapBoxHits)
            {
                Bounds bounds = collider2D.bounds;
                if (bounds.max.x < worldTopRight.x && bounds.max.y < worldTopRight.y && bounds.min.x > worldBottomLeft.x &&
                    bounds.min.y > worldBottomLeft.y)
                {
                    fullyInsideTags.Add(collider2D.tag);
                    collider2D.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                }
                else
                {
                    partlyInsideTags.Add(collider2D.tag);
                    collider2D.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                }
            }

            DetectObjectives(fullyInsideTags, partlyInsideTags);
        }

        /// <summary>
        /// detect if the photo meets the level objective
        /// </summary>
        /// TODO: fix
        private void DetectObjectives(List<string> tagsFullyInPhoto, List<string> tagsPartlyInPhoto)
        {
            LevelObjectivesSO.LevelObjective currentLevelObjective = GameManager.Instance.levelManager.currentLevelObjective;

            foreach (var objTag in tagsPartlyInPhoto)
            {
                if (currentLevelObjective.failingObjectiveTags.Contains(objTag))
                {
                    // TODO: show wrong target panel
                    print("wrong target in photo");
                    GameManager.Instance.ChangeState(GameManager.GameState.LevelFailed);
                    return;
                }
            }

            foreach (var objTag in tagsFullyInPhoto)
            {
                if (currentLevelObjective.winningObjectiveTags.Contains(objTag))
                {
                    currentLevelObjective.winningObjectiveTags.Remove(objTag);
                }
            }

            if (currentLevelObjective.winningObjectiveTags.Count == 0)
            {
                // TODO: show the completed objective
                print("an objective completed");
                GameManager.Instance.ChangeState(GameManager.GameState.LevelCompleted);
            }
            else
            {
                // TODO: show not enough target panel
                print("not all targets in photo");
                GameManager.Instance.ChangeState(GameManager.GameState.LevelFailed);
            }
        }

        // private void OnDrawGizmos()
        // {
        //     Vector3[] corners = new Vector3[4];
        //     _rectTransform.GetWorldCorners(corners);
        //
        //     // 这些角点是屏幕空间的坐标，对于Overlay模式，它们相当于屏幕坐标
        //     Vector3 screenBottomLeft = corners[0];
        //     Vector3 screenTopRight = corners[2];
        //
        //     // 将屏幕坐标转换为世界坐标
        //     Vector3 worldBottomLeft = Camera.main.ScreenToWorldPoint(screenBottomLeft);
        //     Vector3 worldTopRight = Camera.main.ScreenToWorldPoint(screenTopRight);
        //
        //     // 计算中心点和大小
        //     Vector3 center = (worldBottomLeft + worldTopRight) / 2;
        //     Vector2 size = new Vector2(Mathf.Abs(worldTopRight.x - worldBottomLeft.x), Mathf.Abs(worldTopRight.y - worldBottomLeft.y));
        //
        //     Gizmos.color = Color.green;
        //     Gizmos.DrawWireCube(center, size);
        // }
    }
}
