using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Checkpoints
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField] private bool _isCounting;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private float _delayFlash;
        [Space(15)]        
        [SerializeField] private GameManager.PlatformerManager _manager;

        private SpriteRenderer _sprite;

        public BoxCollider2D Collider { get => _collider; }

        private void Start()
        {
            _collider = this.gameObject.GetComponent<BoxCollider2D>();
            _manager = FindObjectOfType<GameManager.PlatformerManager>();
            _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        }
        private void GoToNextScene()
        {
            GameManager.LevelsManager.LevelsManager.FinishScene();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(_manager.PlayerTag) && !_collider.isTrigger)
            {
                _manager.GetComponent<Platformer.MessageEventUI.MessageLvlController>().PlayerTryUse(this.gameObject);
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_manager.PlayerTag))
            {
                GoToNextScene();
            }
        }
        public void ActivateExit()
        {
            _collider.isTrigger = true;
            StartCoroutine(_manager.FlashObject(_delayFlash, _sprite));
        }
    }
}

