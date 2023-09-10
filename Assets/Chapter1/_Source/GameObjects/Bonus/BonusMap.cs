using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;
using TMPro;
namespace Bonus
{
    public class BonusMap : BonusItem
    {
        [Header("Values")]
        [SerializeField] private float _timeAddValue;
        [Header("Links")]
        [SerializeField] private GameUI.GameUI _gameUI;
        [SerializeField] private TextMeshProUGUI _txtAddValue;
        private TimerCount _timer;
        private void Start()
        {
            _txtAddValue = GetComponentInChildren<TextMeshProUGUI>();
            _txtAddValue.text = _timeAddValue.ToString();
            if (_gameUI == null)
            {
                _gameUI = FindObjectOfType<GameUI.GameUI>();
            }
            _timer = _gameUI.Timer;
        }
        protected override void Taked(GameObject player)
        {
            _timer.AddTime(_timeAddValue);
            _txtAddValue.enabled = false;
            base.Taked(player);
        }
    }
}

