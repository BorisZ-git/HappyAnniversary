using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCamera;

public class LevelCameraLogic
{
    private CameraMove2D _cameraMove;
    private GameObject _objToCentrelazied;
    private float _zoomIn;
    private float _zoomOut;
    public LevelCameraLogic(CameraMove2D camera, GameObject objToCentrelazied, float zoomIn, float zoomOut)
    {
        _cameraMove = camera;
        _objToCentrelazied = objToCentrelazied;
        _zoomIn = zoomIn;
        _zoomOut = zoomOut;
    }
    public void CameraRideStarting()
    {
        _cameraMove.Follow = false;
        _cameraMove.SetZoom(_zoomOut);
    }
    public void CameraRideOff()
    {
        _cameraMove.Follow = true;
        _cameraMove.SetZoom(_zoomIn);
    }
}
