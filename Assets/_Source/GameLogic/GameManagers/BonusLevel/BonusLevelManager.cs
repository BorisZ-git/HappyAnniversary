using System.Collections;
using UnityEngine;
using BonusLevel.RidePlatform;

public class BonusLevelManager : MonoBehaviour
{
    [Header("Level Move Property")]
    [SerializeField] private float _speed;
    [SerializeField] private Transform _level;
    [SerializeField] private float _timeToStop;
    [SerializeField] private float _moveFreeze;
    [Header("Camera Zoom Effect")]
    [SerializeField] private float _zoomIn;
    [SerializeField] private float _zoomOut;
    [Header("Event Links")]
    [SerializeField] private RidePlatform _ridePlatform;
    [SerializeField] private SwitchBehaviour _switcher;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameCamera.CameraMove2D _camera;

    private LevelMovement _lvlMovement;
    private LevelCameraLogic _lvlCamera;
    private bool _isLevelMove;
    private bool _isLevelStoping;
    private bool _isLevelStartMoving;
    private Transform _point;
    public bool IsLevelMove { get => _isLevelMove; }
    public bool IsLevelStoping { get => _isLevelStoping; }
    public bool IsLevelStartMoving { get => _isLevelStartMoving; }

    private void Awake()
    {
        if(_level == null)
        {
            _level = GetComponent<Transform>();
        }
        if(_ridePlatform == null)
        {
            _ridePlatform = FindObjectOfType<RidePlatform>();
        }
        if(_switcher == null)
        {
            _switcher = FindObjectOfType<SwitchBehaviour>();
        }
        if(_inputManager == null)
        {
            _inputManager = FindObjectOfType<InputManager>();
        }
        if(_camera == null)
        {
            _camera = FindObjectOfType<GameCamera.CameraMove2D>();
        }
        _lvlMovement = new LevelMovement(_level, _speed, _moveFreeze);
        _lvlCamera = new LevelCameraLogic(_camera, _ridePlatform.gameObject, _zoomIn, _zoomOut);
    }
    private void Update()
    {
        if (_isLevelMove)
        {
            _lvlMovement.LevelMove();
            if (_isLevelStoping)
            {
                _lvlMovement.DecreseSpeed();
            }
            if (_isLevelStartMoving)
            {
                _isLevelStartMoving = _lvlMovement.IncreaseSpeed();
            }
        }
    }
    public void LevelStopMove(Transform pointTransfer, Transform pointStable)
    {
        _point = pointTransfer;
        _isLevelStoping = true;
        _inputManager.SetGameInputOff();
        _ridePlatform.StoppingPlatform(pointStable);
        _lvlCamera.CameraRideOff();
        StartCoroutine(StopingMove());
    }
    public void LevelStartMove()
    {
        _isLevelMove = true;
        _isLevelStartMoving = true;
        _lvlCamera.CameraRideStarting();
        _ridePlatform.TurnEngine();
    }
    IEnumerator StopingMove()
    {
        yield return new WaitForSeconds(_timeToStop);
        if (_switcher.isOn)
        {
            _switcher.Switch();
        }
        else
        {
            _inputManager.ChangeToPlayer();
        }
        _isLevelStoping = false;
        _isLevelMove = false;
        StartCoroutine(_switcher.DeactivateSwitch(_timeToStop*3));
        StartCoroutine(_ridePlatform.EndRide(_point));
    }
}
