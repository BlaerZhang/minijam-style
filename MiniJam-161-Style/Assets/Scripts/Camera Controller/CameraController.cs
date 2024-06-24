using System;
using System.Collections.Generic;
using System.Linq;
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
            if (!GameManager.Instance.allowInput) return;

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
            cameraFrameController.ChangeToAimingFrameObject();
            _currentFrameSize = defaultCameraFrameSize;
            _rectTransform.sizeDelta = defaultCameraFrameSize;
            cameraFrameController.UpdateSize(_currentFrameSize);
        }

        private void StopAiming()
        {
            print("Stop aiming");
            _isAiming = false;
            cameraFrameController.ChangeToDefaultFrameObject();

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
                    //check if being covered over certain percentage
                    List<Collider2D> possibleCover = new List<Collider2D>();
                    possibleCover = Physics2D.OverlapBoxAll(collider2D.transform.position, bounds.size, 0).ToList();
                    float coveredPercentage = 0f;
                    foreach (var coverCollider in possibleCover)
                    {
                        if (coverCollider.transform.position.y < collider2D.transform.position.y)
                        {
                            Bounds coverBounds = coverCollider.bounds;
                            float minX = Mathf.Max(bounds.min.x, coverBounds.min.x);
                            float minY = Mathf.Max(bounds.min.y, coverBounds.min.y);
                            float maxX = Mathf.Min(bounds.max.x, coverBounds.max.x);
                            float maxY = Mathf.Min(bounds.max.y, coverBounds.max.y);

                            float intersectionWidth = maxX - minX;
                            float intersectionHeight = maxY - minY;
                            if (intersectionWidth > 0 && intersectionHeight > 0)
                            {
                                float intersectionArea = intersectionWidth * intersectionHeight;
                                float currentCoveredPercentage = intersectionArea / (bounds.size.x * bounds.size.y);
                                coveredPercentage = coveredPercentage < currentCoveredPercentage
                                    ? currentCoveredPercentage
                                    : coveredPercentage;
                            }
                        }
                    }

                    switch (coveredPercentage)
                    {
                        case >= 1:
                            break;
                        case >= 0.5f:
                            partlyInsideTags.Add(collider2D.tag);
                            collider2D.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                            break;
                        case >= 0:
                            fullyInsideTags.Add(collider2D.tag);
                            collider2D.GetComponentInChildren<SpriteRenderer>().color = Color.green;
                            break;
                    }
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
        private void DetectObjectives(List<string> tagsFullyInPhoto, List<string> tagsPartlyInPhoto)
        {
            // prepare the objective data
            LevelObjectivesSO.LevelObjective currentLevelObjective = GameManager.Instance.levelManager.currentLevelObjective;

            List<string> winningObjectiveTags = new List<string>();
            List<string> failingObjectiveTags = new List<string>();

            foreach (var objTag in currentLevelObjective.winningObjectiveTags)
            {
                winningObjectiveTags.Add(objTag);
            }
            foreach (var objTag in currentLevelObjective.failingObjectiveTags)
            {
                failingObjectiveTags.Add(objTag);
            }

            // check partly in-photo targets
            foreach (var objTag in tagsPartlyInPhoto)
            {
                if (failingObjectiveTags.Contains(objTag))
                {
                    // TODO: show wrong target panel
                    print("wrong target in photo");
                    GameManager.Instance.ChangeState(GameManager.GameState.LevelFailed);
                    return;
                }
            }

            // check fully in-photo targets
            foreach (var objTag in tagsFullyInPhoto)
            {
                if (failingObjectiveTags.Contains(objTag))
                {
                    // TODO: show wrong target panel
                    print("wrong target in photo");
                    GameManager.Instance.ChangeState(GameManager.GameState.LevelFailed);
                    return;
                }

                if (winningObjectiveTags.Contains(objTag))
                {
                    winningObjectiveTags.Remove(objTag);
                    print("a target found");
                }
            }

            // check if all targets are found
            if (winningObjectiveTags.Count == 0)
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
