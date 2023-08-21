using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
namespace GameUI
{
    public class TimerCount : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private Text _timerView;
        [SerializeField] private LevelEffectManager _lvlEffectManager;
        [Header("Numeric values")]
        [SerializeField] private float _delayTime = 1;
        [SerializeField] private float _warningTime;
        [SerializeField] private string _hashRestartLevel;
        private float _time;
        private bool _isTimeEnd;
        private bool _isLevelStarted;
        private bool _countFlag;
        private int _minutes, _seconds;
        private string _timeFormat, _minString, _secString;

        public bool IsLevelStarted { get => _isLevelStarted; set => _isLevelStarted = value; }

        void Start()
        {
            if (_timerView == null)
            {
                _timerView = GetComponentInChildren<Text>();
            }
            if(_lvlEffectManager == null)
            {
                _lvlEffectManager = FindObjectOfType<LevelEffectManager>();
            }
        }

        void Update()
        {
            if (!_isTimeEnd && IsLevelStarted)
            {
                Count();
            }
        }
        IEnumerator CountWait()
        {
            yield return new WaitForSeconds(_delayTime);
            _countFlag = true;
        }
        public void ActivateTimer(float time)
        {
            _time = time;
            _countFlag = true;
            UpdateView();
        }
        public void DeactivateTimer()
        {
            gameObject.SetActive(false);
        }
        public void AddTime(float addTimeValue)
        {
            _time += addTimeValue;
            if (_time > _warningTime)
            {
                _lvlEffectManager.WarningTimeOff();
                _timerView.color = Color.white;
            }
            UpdateView();
        }
        private void Count()
        {
            _time -= Time.deltaTime;
            if (_countFlag)
            {
                _countFlag = false;
                UpdateView();
                StartCoroutine(CountWait());
            }
        }
        private void SetTimeFormat()
        {
            _minutes = ((int)(_time / 60));
            if (_minutes > 0)
            {
                _seconds = ((int)_time - _minutes * 60);
            }
            else
            {
                _seconds = (int)_time;
            }
        }
        private void TimerFormat()
        {
            SetTimeToString(_minutes, out _minString);
            SetTimeToString(_seconds, out _secString);
            _timeFormat = string.Format("{0}:{1}", _minString, _secString);
        }
        private void UpdateView()
        {
            if (_time > 0)
            {
                SetTimeFormat();
                TimerFormat();
                _timerView.text = _timeFormat;
                if(_time <= _warningTime + 1)
                {
                    _lvlEffectManager.WarningTimeON();
                }
            }
            else if (_time <= 0)
            {
                _timerView.text = "Time is End";
                _isTimeEnd = true;
                StopAllCoroutines();
                StartCoroutine(_lvlEffectManager.TimeIsEnd());
            }
        }

        private void SetTimeToString(float time, out string set)
        {
            if (time >= 10)
            {
                set = string.Format("{0}", time);
            }
            else
            {
                set = string.Format("0{0}", time);
            }
        }
    }
}


