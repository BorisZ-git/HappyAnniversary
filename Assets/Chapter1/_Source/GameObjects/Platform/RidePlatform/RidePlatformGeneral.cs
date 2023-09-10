using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BonusLevel.RidePlatform
{
    [RequireComponent(typeof(AudioSource))]
    abstract public class RidePlatformGeneral : InputGeneralBehaviour
    {
        [Header("Links")]
        [SerializeField] protected SwitchBehaviour _switcher;
        [Header("Layers")]
        [SerializeField] protected LayerMask _movingObjects;
        [SerializeField] protected LayerMask _playerMask;
        [SerializeField] protected LayerMask _enemyMask;
        [Header("Audio")]
        [SerializeField] protected AudioClip _acEngineOn;
        [SerializeField] protected AudioClip _acEngineOff;
        [SerializeField] protected AudioClip _acMove;

        protected AudioSource _audioSource;
        protected PlatformMove _platformMove;
        virtual protected void Awake()
        {
            if(GameManager.LevelsManager.LevelsManager.ridePlatformInput == null)
            {
                _input = new PlatformInput(this, new RidePlatformControls());
                GameManager.LevelsManager.LevelsManager.ridePlatformInput = _input;
            }
            else
            {
                _input = GameManager.LevelsManager.LevelsManager.ridePlatformInput;
                (_input as PlatformInput).SetInput(this);
            }
            if (_switcher == null)
            {
                _switcher = GetComponentInChildren<SwitchBehaviour>();
            }
            _audioSource = GetComponent<AudioSource>();
        }
        protected void FixedUpdate()
        {
            _platformMove.Move(_vectorInput * Time.deltaTime * _speed);
        }
        public void Use()
        {
            if (_switcher != null)
            {
                _switcher.Switch();
            }
        }
        public void AudioMove(Vector2 vector)
        {
            if (vector.x != 0 || vector.y != 0)
            {
                _audioSource.clip = _acMove;
                _audioSource.loop = true;
                _audioSource.Play();
            }
            else
            {
                _audioSource.loop = false;
                _audioSource.Stop();
            }
        }            
    }
}

