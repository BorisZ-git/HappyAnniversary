using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
namespace GameUI
{
    public class PlayerStatus : Status
    {
        [Header("Max Status Values")]
        [SerializeField] private int _maxHp;
        [SerializeField] private int _maxMp;
        [Header("Links")]
        [SerializeField] private GameUI _gameUI;
        private GameManager.LevelEffectManager _lvlEffetcManager;
        private PlatformerManager _lvlManager;
        private Player.Player _player;

        protected override void Start()
        {
            if(_gameUI == null)
            {
                _gameUI = FindObjectOfType<GameUI>();
            }
            if(HpBar == null)
            {
                HpBar = _gameUI.HPBar;
            }
            if(MpBar == null)
            {
                MpBar = _gameUI.MPBar;
            }
            _lvlEffetcManager = FindObjectOfType<GameManager.LevelEffectManager>();
            _lvlManager = FindObjectOfType<PlatformerManager>();
            _player = GetComponent<Player.Player>();
            base.Start();
        }

        override public void SetHP(int value)
        {
            if(value > 0 && Hp != _maxHp || value < 0)
            {
                base.SetHP(value);
            }
            if (Hp <= 0)
            {
                LooseHP();
            }
        }
        override public void SetMP(int value)
        {
            if (value > 0 && Mp != _maxMp || value < 0)
            {
                base.SetMP(value);
            }
        }
        protected override void LooseHP()
        {
            _player.LooseAllHP();
        }

        protected override void Hurt()
        {
            _player._asPlayer.AudioHurt();
            _lvlEffetcManager.PlayerHurted();
            _lvlManager.PlayerHurtMsgPlay();
        }
    }
}

