using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Platformer.MessageEventUI
{
    [RequireComponent(typeof(TalkerSpritesData))]
    [RequireComponent(typeof(MessageLayersData))]
    public class MessageEventUI : MonoBehaviour
    {
        [Header("Numeric Values")]
        [SerializeField] private float _delayPrint;
        [Header("Links")]
        [SerializeField] private Image _leftSpriteTalker;
        [SerializeField] private GameObject _leftMessageUI;
        [SerializeField] private Text _leftTextUI;
        [SerializeField] private Image _rightSpriteTalker;
        [SerializeField] private GameObject _rightMessageUI;
        [SerializeField] private Text _rightTextUI;

        private Image _spriteTalker;
        private GameObject _messageUI;
        private Text _textUI;

        private bool _isPrint;
        private string _textMessage;
        private float _readTime;
        private bool _isSpeakReplic;

        public bool isSpeakReplic { get => _isSpeakReplic; set => _isSpeakReplic = value; }
        public Image SpriteTalker { get => _spriteTalker; }

        /// <summary>
        /// Starting show message
        /// </summary>
        /// <param name="talker">Who'll be talking, use MessageEventUI class sprite for default talker</param>
        /// <param name="message">what will be told</param>
        /// <param name="isPrint">False if you want text appears instantly</param>
        /// <param name="readTime">How long you can see UIMessage</param>
        public void SetMessage(Sprite talker, string message, bool isPrint, float readTime, bool isLeftSpeaker)
        {
            StopAllCoroutines();
            _isSpeakReplic = true;
            if (isLeftSpeaker)
            {
                SetLeftSpeaker();
            }
            else
            {
                SetRightSpeaker();
            }
            ActivateTalk();
            _spriteTalker.sprite = talker;
            _textMessage = message;
            _isPrint = isPrint;
            _readTime = readTime;
            _textUI.text = string.Empty;
            StartCoroutine(PrintText());
        }
        IEnumerator PrintText()
        {
            if (_isPrint)
            {
                for (int i = 0; i < _textMessage.Length; i++)
                {
                    _textUI.text += _textMessage[i];
                    yield return new WaitForSeconds(_delayPrint);
                }
            }
            else
            {
                _textUI.text = _textMessage;
            }
            yield return new WaitForSeconds(_readTime);
            DeactiveteTalk();
        }
        private void ActivateTalk()
        {
            _spriteTalker.gameObject.SetActive(true);
            _messageUI.gameObject.SetActive(true);
        }
        public void DeactiveteTalk()
        {
            _spriteTalker.gameObject.SetActive(false);
            _messageUI.gameObject.SetActive(false);
            _textMessage = string.Empty;
            _textUI.text = string.Empty;
            _isPrint = false;
            _isSpeakReplic = false;
        }
        public void SetLeftSpeaker()
        {
            _spriteTalker = _leftSpriteTalker;
            _messageUI = _leftMessageUI;
            _textUI = _leftTextUI;
        }
        public void SetRightSpeaker()
        {
            _spriteTalker = _rightSpriteTalker;
            _messageUI = _rightMessageUI;
            _textUI = _rightTextUI;
        }
    }

}
