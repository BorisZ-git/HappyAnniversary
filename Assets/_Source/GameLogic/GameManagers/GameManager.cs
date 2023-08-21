using UnityEngine;
using GameUI;
using Platformer.MessageEditor;
using Platformer.MessageEventUI;
namespace GameManager
{
    /// <summary>
    /// Find and Create MechanickManager in Scene
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game Mechanic")]
        [SerializeField] private bool _platformer;
        [SerializeField] private bool _arcade;
        [SerializeField] private bool _puzzle;
        [SerializeField] private bool _threeRow;
        [SerializeField] private bool _quest;
        [Header("Level Settings")]
        [SerializeField] private bool _isStoreFinish;
        [SerializeField] private bool _isBossLevel;
        [SerializeField] private bool _isBonusLevel;
        [Header("Prefabs")]
        [SerializeField] private GameObject _platformerManager;
        [SerializeField] private GameObject _arcadeManager;
        [SerializeField] private GameObject _puzzleManager;
        [SerializeField] private GameObject _threeRowManager;
        [SerializeField] private GameObject _questManager;
        [Header("Timer property")]
        [SerializeField] private bool _isTimeLevel;
        [SerializeField] private float _time;
        [Header("Links")]
        [SerializeField] private GameUI.GameUI _gameUI;
        [SerializeField] private TimerCount _timer;
        [SerializeField] private GameObject _platformerMng;
        [SerializeField] private BonusLevelManager _bnsLvlMng;
        [SerializeField] private AudioHash _audioHash;
        public bool IsTimeLevel { get => _isTimeLevel;  }
        public bool IsBossLvl { get => _isBossLevel; }

        private void Awake()
        {
            if (_platformer)
            {
                _platformerManager = Instantiate(_platformerManager, gameObject.transform);
                if (_isStoreFinish)
                {
                    _platformerManager.GetComponent<PlatformerManager>().SetFinish();
                }
                else if(!_isStoreFinish && !_isBonusLevel)
                {
                    _platformerManager.GetComponent<PlatformerManager>().SetExit();
                }
                if (_isBonusLevel || _isBossLevel)
                {
                    _bnsLvlMng = FindObjectOfType<BonusLevelManager>();
                }
                _audioHash?.SetAudioLevel(_isBonusLevel, _isBossLevel);
            }
            else
            {
                print("Not set game mechanic in GameManager");
            }
            if (_gameUI == null)
            {
                _gameUI = FindObjectOfType<GameUI.GameUI>();
            }
            if(_timer == null)
            {
                _timer = _gameUI.Timer;
            }
            if (_isTimeLevel)
            {
                    _timer.ActivateTimer(_time);
            }
            else if (!_isTimeLevel)
            {
                _timer.DeactivateTimer();
            }
            LevelProgress.SaveProgress();
        }


    }
}

