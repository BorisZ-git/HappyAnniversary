using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.MessageEventUI;
namespace InteractibleObj
{
    public class Button : MonoBehaviour
    {
        [Header("Time Logic")]
        [SerializeField] private bool _isTimable;
        [SerializeField] private float _useTime;

        [Header("Links")]
        [SerializeField] private Animator _animator;
        [SerializeField] private ButtonActivateObject _activingObject;

        private readonly int _isUseHash = Animator.StringToHash("IsUse"); 
        private bool _isPush;
        private AudioSource _audioSource;

        public bool IsPush { get => _isPush; }

        private void Awake()
        {
            if(_animator == null)
            {
                _animator = gameObject.GetComponent<Animator>();
            }
            if(_activingObject == null)
            {
                Debug.Log("Forget about _activingObject in {0}" + gameObject.name);
            }
            _audioSource = GetComponent<AudioSource>();
        }
        public bool PushButton()
        {
            if (!IsPush)
            {
                _audioSource.Play();
                _isPush = true;
                ActivateObject();
                if (_isTimable)
                {
                    StartCoroutine(TimeCount());
                }
                return true;
            }
            return false;
        }
        IEnumerator TimeCount()
        {
            yield return new WaitForSeconds(_useTime);
            _isPush = false;
            DeactivateObject();
        }
        private void ActivateObject()
        {
            _activingObject.IsActiveted = true;
            _animator.SetBool(_isUseHash, IsPush);
        }
        private void DeactivateObject()
        {
            _activingObject.IsActiveted = false;
            _animator.SetBool(_isUseHash, IsPush);
        }
    }
}

