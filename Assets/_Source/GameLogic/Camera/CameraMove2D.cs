using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameCamera
{
    public class CameraMove2D : MonoBehaviour
    {
        [SerializeField] private bool _follow;
        [Header("Numeric Values")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _limitOffsetX;
        [SerializeField] private float _limitOffsetY;

        [Header("Links")]
        [SerializeField] private GameObject _player;
        [SerializeField] private string _playerTag;

        [Header("Camera Limit Values")]
        [SerializeField] private float _leftLimit;
        [SerializeField] private float _rightLimit;
        [SerializeField] private float _upLimit;
        [SerializeField] private float _bottomLimit;
        private CameraFollow _cameraFollow;
        private CameraShake _cameraShake;
        private GameObject _anotherFollowObj;
        private bool _cameraZoom;
        private bool _isShake;
        private float _zoomValue;
        private float _shakeTime;
        public bool Follow { get => _follow; set => _follow = value; }
        public bool CameraCentrelazied { get; set; }

        private void Awake()
        {
            if (string.IsNullOrEmpty(_playerTag))
            {
                if (FindObjectOfType<Player.Player>())
                {
                    _playerTag = FindObjectOfType<Player.Player>().gameObject.tag;
                }
                else
                {
                    print("not find player tag set default: Player");
                    _playerTag = "Player";
                }
            }
            _player = GameObject.FindGameObjectWithTag(_playerTag);
            _cameraFollow = new CameraFollow(this, _moveSpeed, _leftLimit, _rightLimit, _bottomLimit, _upLimit, _limitOffsetX,_limitOffsetY);
            _cameraShake = new CameraShake(transform);
        }
        private void Update()
        {
            if (_follow)
            {
                if (_player)
                {
                    _cameraFollow.GetTargetVector3(_player);
                }
                _cameraFollow.SetCameraPosition();
                _cameraFollow.OffsetCamera();
            }
            if (_cameraZoom)
            {
                _cameraZoom = _cameraFollow.SetCameraView(_zoomValue);
            }
            if (_isShake)
            {
                if(_shakeTime > 0)
                {
                    _cameraShake.Shake();
                    _shakeTime -= Time.deltaTime;
                }
                else
                {
                    _isShake = false;
                }
            }
        }
        public void ResetPosition(float x, float y, float z)
        {
            this.transform.position = new Vector3(x, y, z);
        }
        public void SetCentrelaziedObject(GameObject obj)
        {
            _anotherFollowObj = obj;
        }
        public void SetZoom(float zoomValue)
        {
            _zoomValue = zoomValue;
            _cameraZoom = true;
        }
        public void Shake(float time)
        {
            _isShake = true;
            _shakeTime = time;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            // from left to right up
            Gizmos.DrawLine(new Vector3(_leftLimit, _upLimit), new Vector3(_rightLimit, _upLimit));
            // from left to right bottom
            Gizmos.DrawLine(new Vector3(_leftLimit, _bottomLimit), new Vector3(_rightLimit, _bottomLimit));
            // from up to bottom left
            Gizmos.DrawLine(new Vector3(_leftLimit, _upLimit), new Vector3(_leftLimit, _bottomLimit));
            // from up to bottom right
            Gizmos.DrawLine(new Vector3(_rightLimit, _upLimit), new Vector3(_rightLimit, _bottomLimit));
        }
    }
}

