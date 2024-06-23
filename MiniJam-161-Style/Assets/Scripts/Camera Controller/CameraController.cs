using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private RectTransform _rectTransform;
    private bool isAiming = false; 
    public CameraFrameController cameraFrameController;
    private Vector2 _currentFrameSize;
    public float zoomDelta = 100f;
    public Vector2 cameraXLimits;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _currentFrameSize = new Vector2(300, 200);
    }
    
    void Update()
    {
        // follow mouse
        transform.position = Input.mousePosition;
        
        // aim
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            _rectTransform.sizeDelta = _currentFrameSize;
            cameraFrameController.UpdateSize(_currentFrameSize);
        }

        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            _rectTransform.sizeDelta = Vector2.zero;
            cameraFrameController.UpdateSize(Vector2.zero);
        }
        
        // shoot
        if (isAiming && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        
        // zoom
        if (isAiming)
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
    
    List<List<string>> Shoot()
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
        
        //Raycast
        Collider2D[] overlapBoxHits = Physics2D.OverlapBoxAll(center, size, 0, LayerMask.GetMask("Shootable"));
        
        //check if fully inside
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

        //Feedback

        List<List<string>> returnList = new List<List<string>>();
        returnList.Add(fullyInsideTags);
        returnList.Add(partlyInsideTags);
        return returnList;
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
