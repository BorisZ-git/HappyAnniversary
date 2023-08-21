using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.MessageEventUI;
using GameManager;

namespace GameUI
{
    public class GameUI : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private GameObject _hpBar;
        [SerializeField] private GameObject _mpBar;
        [SerializeField] private TimerCount _timer;
        [SerializeField] private MessageEventUI _msgEventUI;
        [SerializeField] private Animator _levelsAnimator;
        [SerializeField] private Animator _timerAnimator;
        [SerializeField] private GameManager.LevelEffectManager _lvlEffectManager;


        public GameObject HPBar { get => _hpBar; set => _hpBar = value; }
        public GameObject MPBar { get => _mpBar; set => _mpBar = value; }
        public TimerCount Timer { get => _timer; set => _timer = value; }
        public MessageEventUI MsgEventUI { get => _msgEventUI; set => _msgEventUI = value; }
        //
        // Проверить работоспособность ссылок на объекты
        //
        public Animator LevelsAnimator { get => _levelsAnimator; set => _levelsAnimator = value; }
        public Animator TimerAnimator { get => _timerAnimator; set => _timerAnimator = value; }
        public LevelEffectManager LvlEffectManager { get => _lvlEffectManager; set => _lvlEffectManager = value; }

    }
}

