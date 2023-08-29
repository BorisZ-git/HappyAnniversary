using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.MessageEventUI;
using Supporting;
using Supporting.MessagePath;
using System.Linq;

namespace GameManager
{
    [RequireComponent(typeof(MessageLvlController))]

    public class PlatformerManager : Manager
    {
        [Header("Level Goals")]
        [SerializeField] private bool _isCheckPointed;
        [SerializeField] private bool _isBossLevel;
        [SerializeField] private bool _isStoreFinishLevel;
        [SerializeField] private bool _isBonusLevel;

        [Header("Links")]
        [SerializeField] private Player.Player _player;
        [SerializeField] private string _playerTag;
        [SerializeField] private string _playerUseZoneTag;
        [SerializeField] private Sprite _talkerPlayer;
        [SerializeField] private MessageLvlController _msgLvlController;
        [SerializeField] private LevelEffectManager _lvlEffMng;
        [Space(15)]
        [Header("TEST ERROR")]
        [SerializeField] private Checkpoints.ExitPoint _exit;
        [SerializeField] private int _checkPointsCount;

        #region Property
        public string PlayerTag { get => _playerTag; }
        public string PlayerUseZoneTag { get => _playerUseZoneTag; }
        public int CheckPointsCount { get => _checkPointsCount; }
        public MessageLvlController MsgLvlController { get => _msgLvlController; }
        #endregion

        private void Awake()
        {
            RequirementCheck();         
            _msgLvlController = GetComponent<MessageLvlController>();
        }
        private void RequirementCheck()
        {
            _player = FindAnyObjectByType<Player.Player>();
            _talkerPlayer = _player.GetComponentInChildren<SpriteRenderer>().sprite;
            _lvlEffMng = FindAnyObjectByType<LevelEffectManager>();

            if (string.IsNullOrEmpty(_playerTag))
            {                
                if (_player)
                {
                    _playerTag = _player.gameObject.tag;
                }
                else
                {
                    Debug.Log("Wrong Player tag");
                }
            }
            if (string.IsNullOrEmpty(_playerUseZoneTag))
            {
                
                if (_player)
                {
                    _playerUseZoneTag = _player.GetComponentInChildren<Player.PlayerUse>().gameObject.tag;
                }
                else
                {
                    Debug.Log("Wrong Player Use Zone tag");
                }
            }
        }
        private void CountCheckPoints()
        {
            _checkPointsCount = FindObjectsOfType<Checkpoints.Store>().Length;
        }
        public void SetFinish()
        {
            List<Checkpoints.Store> stores = FindObjectsOfType<Checkpoints.Store>().ToList();
            stores[Random.Range(0, stores.Count)].IsFinish = true;
            _checkPointsCount = stores.Count;
        }
        public void SetExit()
        {
            CountCheckPoints();
            _exit = FindObjectOfType<Checkpoints.ExitPoint>();
        }
        public void CheckGoal()
        {
            _checkPointsCount--;
            if (_checkPointsCount < 1)
            {
                _exit.ActivateExit();
            }
        }
        public void PlayerHurtMsgPlay()
        {
            _msgLvlController.ShowRndMessage(_talkerPlayer, MessagePathStore.PlayerHurt);
        }
        public void PlayerLooseAllHPMsg()
        {
            _msgLvlController.ShowMessage(_talkerPlayer, MessagePathStore.PlayerLooseHp, 0);
        }
        public void PlayerFindStore()
        {
            _msgLvlController.ShowMessage(_talkerPlayer, MessagePathStore.PlayerFindStore, 0);
            _player.SetOff();
            foreach (var item in GameObject.FindObjectsOfType<Enemy.EnemyBehaviour>().ToList())
            {
                item.enabled = false;                
            }
            StartCoroutine(_lvlEffMng.FinishLevelCompleted()); 
        }
        /// <summary>
        /// Change color(grey/white) of sprite with delay Time
        /// </summary>
        /// <param name="delayFlash"></param>
        /// <param name="sprite">What sprite will color change</param>
        /// <returns></returns>
        public IEnumerator FlashObject(float delayFlash, SpriteRenderer sprite)
        {
            while (true)
            {
                if (sprite.color == Color.white)
                {
                    sprite.color = Color.grey;
                }
                else
                {
                    sprite.color = Color.white;
                }
                yield return new WaitForSeconds(delayFlash);
            }            
        }
    }
}
