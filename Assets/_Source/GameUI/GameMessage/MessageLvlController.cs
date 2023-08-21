using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.MessageEditor;
using Supporting;
using Supporting.MessagePath;
using GameManager;
using Checkpoints;
using InteractibleObj;
namespace Platformer.MessageEventUI
{
    public class MessageLvlController : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private MessageEventUI _messageEventUI;
        [SerializeField] private TalkerSpritesData _spriteData;
        [SerializeField] private MessageLayersData _layersData;
        [SerializeField] public Sprite leftTalker;
        [SerializeField] public Sprite rightTalker;

        #region Заметка для себя
        // Возможно всю эту логику лучше было бы реализовать через подписку на метод, то есть как раз создавать Event. Но класс уже написан, он вполне рабочий и перевариваем для работы.
        // Но можно подумать об апгрейде кода.
        #endregion
        private MessageData _messageData;
        private Message _msg;
        private int _elementIndex;
        private Sprite _talker;
        private bool _nextDialogReplic;
        private void Update()
        {
            if(_nextDialogReplic && !_messageEventUI.isSpeakReplic)
            {
                ShowPlotDialog();
            }
        }

        private void Awake()
        {
            _messageEventUI = FindObjectOfType<MessageEventUI>();
            _spriteData = _messageEventUI.gameObject.GetComponent<TalkerSpritesData>();
            _layersData = _messageEventUI.gameObject.GetComponent<MessageLayersData>();
        }
        public void ShowRndMessage(Sprite talker, string filePath)
        {
            //Десериализовывать внутри статичного класса и получать сразу ссылки на бд сообщений, возможно реализовать исключенние повторов сообщений.
            GetComponent<AudioSource>().Play();
            _messageData = WriteLoadText.DeserializeFile(filePath);
            _msg = _messageData.messages[Random.Range(0, _messageData.messages.Count - 1)];
            _messageEventUI.SetMessage(talker, _msg.messageText, _msg.isPrint, _msg.readTime, _msg.isLeftSideSpeaker);
        }
        public void ShowMessage(Sprite talker, string filePath, int elementIndex)
        {
            GetComponent<AudioSource>().Play();
            _messageData = WriteLoadText.DeserializeFile(filePath);
            _msg = _messageData.messages[elementIndex];
            _messageEventUI.SetMessage(talker, _msg.messageText, _msg.isPrint, _msg.readTime, _msg.isLeftSideSpeaker);
        }

        #region PlotMessageLogic
        public void TakePlotMessageData(string filePath)
        {
            _messageData = WriteLoadText.DeserializeFile(filePath);
            _elementIndex = 0;
            _msg = _messageData.messages[_elementIndex];
        }
        /// <summary>
        /// First TakePlotMesageData. Use for Speak => Action => Speak
        /// </summary>
        /// <param name="talker"></param>
        public void ShowPlotMessage(Sprite talker)
        {
            GetComponent<AudioSource>().Play();
            _messageEventUI.SetMessage(talker, _msg.messageText, _msg.isPrint, _msg.readTime, _msg.isLeftSideSpeaker);
            if (++_elementIndex < _messageData.messages.Count) 
            {
                _msg = _messageData.messages[_elementIndex];
                _nextDialogReplic = true;
            }
        }
        /// <summary>
        /// First TakePlotMessageData. Second Find Sprites for speaker sides. Use for Dialog between two characters
        /// </summary>
        public void ShowPlotDialog()
        {
            _nextDialogReplic = false;
            if (_msg.isLeftSideSpeaker)
            {
                _talker = leftTalker;
            }
            else
            {
                _talker = rightTalker;
            }
            if (_elementIndex < _messageData.messages.Count)
            {
                ShowPlotMessage(_talker);
            }
        }

        #endregion
        #region GameLogic
        public void PlayerTryUse(GameObject useObject)
        {
            if (Utils.IsInLayer(useObject.layer, _layersData.StoreMask))
            {
                if (useObject.GetComponent<Store>().IsChecked)
                {
                    ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.StoreIsCheck);
                }
                else if (!useObject.GetComponent<Store>().IsChecked)
                {
                    if (gameObject.GetComponent<PlatformerManager>().CheckPointsCount - 1 < 1)
                    {
                        ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.ExitActivate);
                    }
                    else
                    {
                        ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.CheckStore);
                    }
                }
            }
            else if (Utils.IsInLayer(useObject.layer, _layersData.ButtonMask))
            {
                if (useObject.GetComponent<InteractibleObj.Button>().IsPush)
                {
                    ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.ButtonActive);
                }
                else
                {
                    ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.ButtonActivate);
                }
            }
            else if (Utils.IsInLayer(useObject.layer, _layersData.ExitMask))
            {
                ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.ExitIsFalse);
            }
            else if (Utils.IsInLayer(useObject.layer, _layersData.SwitcherMask))
            {
                print("Switch");
            }
        }
        public void PlayerHurted()
        {
            ShowRndMessage(_spriteData.TalkerAlice, MessagePathStore.PlayerHurt);
        }
        #endregion
    }
}


