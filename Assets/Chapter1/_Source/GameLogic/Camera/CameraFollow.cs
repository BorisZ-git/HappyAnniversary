using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameCamera
{
    public class CameraFollow
    {
        [Header("Numeric Values")]
        private float _moveSpeed;
        private float _limitOffsetX;
        private float _limitOffsetY;

        [Header("Camera Limit Values")]
        private float _leftLimit;
        private float _rightLimit;
        private float _upLimit;
        private float _bottomLimit;

        private Camera _camera;
        private CameraMove2D _cameraMove;
        private Vector3 target, pos;

        public CameraFollow(CameraMove2D camera, float moveSpeed, float leftLimit, float rightLimit, float bottomLimit, float upLimit, float limitOffsetX, float limitOffsetY)
        {
            _cameraMove = camera;
            _camera = _cameraMove.GetComponent<Camera>();
            _moveSpeed = moveSpeed;
            _leftLimit = leftLimit;
            _rightLimit = rightLimit;
            _upLimit = upLimit;
            _bottomLimit = bottomLimit;
            _limitOffsetX = limitOffsetX;
            _limitOffsetY = limitOffsetY;
            SetSceneBordersLimit();
        }
        public void OffsetCamera()
        {
            _cameraMove.transform.position = new Vector3
                (
                Mathf.Clamp(_cameraMove.transform.position.x, _leftLimit, _rightLimit),
                Mathf.Clamp(_cameraMove.transform.position.y, _bottomLimit, _upLimit),
                _cameraMove.transform.position.z
                );
        }
        public void GetTargetVector3(GameObject toFollow)
        {
            target.x = toFollow.transform.position.x;
            target.y = toFollow.transform.position.y;
            target.z = toFollow.transform.position.z - 10;
        }
        public void SetCameraPosition()
        {
            pos = Vector3.Lerp(_cameraMove.transform.position, target, _moveSpeed * Time.deltaTime);
            _cameraMove.transform.position = pos;
        }
        /// <summary>
        /// Change Camera ortographic size to value
        /// </summary>
        /// <param name="value"></param>
        public bool SetCameraView(float value)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, value, _moveSpeed/2 * Time.deltaTime);
            if(_camera.orthographicSize == value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SetSceneBordersLimit()
        {
            _leftLimit += _limitOffsetX;
            _rightLimit -= _limitOffsetX;
            _bottomLimit += _limitOffsetY;
            _upLimit -= _limitOffsetY;
        }

    }
}

