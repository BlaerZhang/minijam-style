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
        if (isAiming & Input.GetMouseButtonDown(0))
        {
            //TODO: Shoot Func
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
}
