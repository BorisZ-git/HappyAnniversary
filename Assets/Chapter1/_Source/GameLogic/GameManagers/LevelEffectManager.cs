using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;

namespace GameManager
{
    public class LevelEffectManager : MonoBehaviour
    {
        [Header("Animation Values")]
        [SerializeField] private float _timeToFadeEffect;
        [SerializeField] private float _timeToEndTime;
        [SerializeField] private float _timeToRestartLevel;
        [SerializeField] private float _timeToFinishScene;
        [Header("Animation Hash")]
        [SerializeField] private string _hashLvlAnimatorRestartLvl;
        [SerializeField] private string _hashTxtAnimatorWarning;
        [SerializeField] private string _hashPlayerHurt;
        [SerializeField] private string _hashIsPlayerLooseAllHP;

        [Header("Links")]
        [SerializeField] private GameUI.GameUI _gameUI;
        [SerializeField] private Player.Player _player;
        [SerializeField] private AudioManager _audioMng;

        [Header("Audio Values")]
        [SerializeField] private float _timeToTransitAudio;

        private Animator _levelsAnimator;
        private Animator _timerAnimator;
        private TimerCount _timer;
        private void Awake()
        {
            if(_player == null)
            {
                _player = FindObjectOfType<Player.Player>();
            }
            if(_gameUI == null)
            {
                _gameUI = FindObjectOfType<GameUI.GameUI>();
            }
            if(_audioMng == null)
            {
                _audioMng = FindObjectOfType<AudioManager>();
            }
            _timer = _gameUI.Timer;
            _timerAnimator = _gameUI.TimerAnimator;
            _levelsAnimator = _gameUI.LevelsAnimator;
        }
        private void Start()
        {            
            StartCoroutine(StartLevel());
        }
        public IEnumerator TimeIsEnd()
        {
            _audioMng.FadeMusic(_timeToTransitAudio);
            _player.Input.Untying();
            yield return new WaitForSeconds(_timeToEndTime);
            _levelsAnimator.SetBool(_hashLvlAnimatorRestartLvl, true);
            yield return new WaitForSeconds(_timeToFadeEffect);
            LevelsManager.LevelsManager.RestartLevel();
        }
        public IEnumerator PlayerHPRestartLevel()
        {
            _audioMng.FadeMusic(_timeToTransitAudio);
            _levelsAnimator.SetTrigger(_hashIsPlayerLooseAllHP);
            yield return new WaitForSeconds(_timeToRestartLevel);
            LevelsManager.LevelsManager.RestartLevel();
        }
        public IEnumerator StartLevel()
        {
            _audioMng.AppearMusic(_timeToTransitAudio);
            yield return new WaitForSeconds(_timeToFadeEffect);
            _timer.IsLevelStarted = true;
        }
        public IEnumerator FinishLevelCompleted()
        {
            _audioMng.FadeMusic(_timeToTransitAudio);
            yield return new WaitForSeconds(_timeToFinishScene);
            LevelsManager.LevelsManager.FinishScene();
        }
        public void WarningTimeON()
        {
            _timerAnimator.SetBool(_hashTxtAnimatorWarning,true);
        }
        public void WarningTimeOff()
        {
            _timerAnimator.SetBool(_hashTxtAnimatorWarning, false);
        }
        public void PlayerHurted()
        {
            _levelsAnimator.SetTrigger(_hashPlayerHurt);
        }
        public void StopAllEffects()
        {
            StopAllCoroutines();
        }
        // При рестарте уровня  ругается что потерял игрока=> контрольинпут игрока и скрипт игрока
        public void TestRestart()
        {
            LevelsManager.LevelsManager.RestartLevel();
        }
    }
}

