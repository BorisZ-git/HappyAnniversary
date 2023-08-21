using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InteractibleObj
{
    public class MovingPlatform : ButtonActivateObject
    {
        [Header("Numeric Values")]
        [SerializeField] private float _movingSpeed;

        [Header("Conditions")]
        [SerializeField] private bool _isMoved;
        [SerializeField] private bool _moveLeftRight;
        [SerializeField] private bool _moveUpDown;
        [SerializeField] private bool _moveCircle;

        [Header("Links")]
        [SerializeField] private Transform _platform;

        [Header("Points Property")]
        [SerializeField] private GameObject _upPoint;
        [SerializeField] private GameObject _downPoint;
        [SerializeField] private GameObject _leftPoint;
        [SerializeField] private GameObject _rightPoint;        

        [Header("Layers")]
        [SerializeField] private LayerMask _movingObjMask;

        private bool _reachPoint;
        private List<GameObject> _points;
        private GameObject _point;

        public LayerMask MovingObjMask { get => _movingObjMask; }

        private void Start()
        {
            _points = new List<GameObject>();
            _points.Add(_leftPoint);
            _points.Add(_upPoint);
            _points.Add(_rightPoint);
            _points.Add(_downPoint);
            if(_platform == null)
            {
                _platform = GetComponentInChildren<Platform>().transform;
            }
        }
        private void Update()
        {
            if (IsActiveted || _isMoved)
            {
                if (_moveUpDown)
                {
                    MovePlatform(_upPoint, _downPoint);
                }
                else if (_moveLeftRight)
                {
                    MovePlatform(_leftPoint, _rightPoint);
                }
                else if (_moveCircle)
                {
                    MoveCircle();
                }
            }
        }
        private void MovePlatform(GameObject point1, GameObject point2)
        {
            if (!_reachPoint)
            {
                _platform.position = Vector2.MoveTowards(_platform.position, point1.transform.position, _movingSpeed * Time.deltaTime);
                if(_platform.position == point1.transform.position)
                {
                    _reachPoint = true;
                }
            }
            else
            {
                _platform.position = Vector2.MoveTowards(_platform.position, point2.transform.position, _movingSpeed * Time.deltaTime);
                if (_platform.position == point2.transform.position)
                {
                    _reachPoint = false;
                }
            }
        }
        private void MoveCircle()
        {
            TakePoint();
            CheckPoint();
            if (!_reachPoint)
            {
                _platform.position = Vector2.MoveTowards(_platform.position, _point.transform.position, _movingSpeed * Time.deltaTime);
                if (_platform.position == _point.transform.position)
                {
                    _reachPoint = true;
                }
            }
            else
            {
                RemovePoint();
                _point = null;
                _reachPoint = false;
            }
        }
        private void TakePoint()
        {
            if (_point == null)
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    if (_points[i].activeInHierarchy)
                    {
                        _point = _points[i];
                        return;
                    }
                }
            }
        }
        private void CheckPoint()
        {
            if (_point == null)
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    _points[i].SetActive(true);
                }
                _point = _points[0];
            }
        }
        private void RemovePoint()
        {
            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i] == _point)
                {
                    _points[i].SetActive(false);
                    return;
                }
            }
        }
    }
}

