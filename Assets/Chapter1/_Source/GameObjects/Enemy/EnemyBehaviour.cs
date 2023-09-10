using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supporting;
using System;

namespace Enemy
{
    // Создать отдельный скрипт ответственный за анимацию 
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(EnemyAnimation))]
    [RequireComponent(typeof(AudioData))]
    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float _speed;
        [SerializeField] private float _rayDistanceGround;
        [SerializeField] private float _rayDistanceBarrier;
        [SerializeField] private float _deadTimeAnim;
        [Header("Links")]
        [SerializeField] private EnemyAnimation _enemyAnimation;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Transform _leftPoint;
        [SerializeField] private Transform _rightPoint;
        [Header("Layers")]
        [SerializeField] private LayerMask _barrier;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private string _deadSortLayer;
        [SerializeField] private string _enemySortLayer;

        private RaycastHit2D _leftGroundHit;
        private RaycastHit2D _rightGroundHit;
        private RaycastHit2D _leftWallHit;
        private RaycastHit2D _rightWallHit;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider2D;
        private EnemyAttack _enemyAttack;        
        private bool _direction;
        private bool _isAttack;

        public AudioData _audioData;
        public bool IsDead { get; set; }
        public bool IsAttack { get => _isAttack; set => _isAttack = value; }
        public EnemyAnimation EnemyAnim { get => _enemyAnimation; }
        public Rigidbody2D rb2D { get => _rigidbody; set => _rigidbody = value; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyAnimation = GetComponent<EnemyAnimation>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
            _enemyAttack = GetComponentInChildren<EnemyAttack>();
            _enemyAnimation.Sprite = _sprite;
            _audioData = GetComponent<AudioData>();
        }
        private void FixedUpdate()
        {
            CheckMove();
        }
        public void CheckMove()
        {
            if (!IsAttack && !IsDead)
            {
                _enemyAnimation.Walk(true);
                _leftGroundHit = Physics2D.Raycast(_leftPoint.position, Vector2.down, _rayDistanceGround, _ground);
                _rightGroundHit = Physics2D.Raycast(_rightPoint.position, Vector2.down, _rayDistanceGround, _ground);
                _leftWallHit = Physics2D.Raycast(transform.position, Vector2.left, _rayDistanceBarrier, _barrier);
                _rightWallHit = Physics2D.Raycast(transform.position, Vector2.right, _rayDistanceBarrier, _barrier);

                if (_leftGroundHit.collider == null || _leftWallHit.collider != null)
                {
                        CheckDirection(true);                    
                }
                if (_rightGroundHit.collider == null || _rightWallHit.collider != null)
                {
                        CheckDirection(false);
                }
                _rigidbody.velocity = _direction ? new Vector2(1 * _speed, _rigidbody.velocity.y) : new Vector2( -1 * _speed,_rigidbody.velocity.y);
            }
        }
        public void CheckFlip(Vector3 PlayerSide)
        {
            _sprite.flipX = PlayerSide.x < transform.position.x ? false : true;
        }
        public void CheckDirection(bool side)
        {
            if (side)
            {
                _direction = true;
                _sprite.flipX = true;
            }
            if (!side)
            {
                _direction = false;
                _sprite.flipX = false;
            }
        }
        public void Dead()
        {
            _sprite.sortingLayerName =_deadSortLayer;
            _collider2D.enabled = false;
            _rigidbody.isKinematic = true;
            _rigidbody.Sleep();
            IsDead = true;
            _enemyAnimation.Dead(IsDead);
            _enemyAttack.Dead();
            StartCoroutine(WaitDeadAnim());
        }
        IEnumerator WaitDeadAnim()
        {
            yield return new WaitForSeconds(_deadTimeAnim);
            gameObject.SetActive(false);
        }
    }
}

