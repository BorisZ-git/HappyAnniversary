using UnityEngine;
using GameUI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class Player : InputGeneralBehaviour
    {
        //speed = 3;
        [Header("Numeric Valuables")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private int _meleeDamage;
        [SerializeField] private float _meleeAttackDistance;
        [SerializeField] private int _distanceDamage;
        [SerializeField] private int _amountBullet;
        [SerializeField] private float _attackDelay;

        [Header("Requirement components")]
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _groundPoint;
        [SerializeField] private Transform _attackDirection;
        [SerializeField] private Transform _spawnFlyKissPoint;

        [Header("Links")]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] public PlayerAudioSource _asPlayer;

        [Header("Layers")]
        [SerializeField] private LayerMask _groundLayers;
        [SerializeField] private LayerMask _enemyLayers;

        private PlayerMovement _movement;
        private PlayerAnimator _playerAnimator;
        private PlayerUse _playerUse;
        private PlayerMeleeAttack _playerMeleeAttack;
        private PlayerDistanceAttack _playerDistanceAttack;
        private Vector2 _inputVector;
        private bool _directionForward, _grounded, _jumping, _isMeleeAttack, _isDistanceAttack;
        private Vector3 _scaleForward, _scaleBackward;
        // Что то я с атакой намудрил, надо переделать логику работы ударов. Проблема в том что залипает на булевой в коде и потом не соответственно нет доступа к коду аттаки
        // Может убрать эвенты из анимации и сделать тупо карутину.
        private float _resetAttackTime;
        public InputParent Input { get => _input; }
        private void Awake()
        {
            RequirementCheck();
            Init();
        }
        private void Update()
        {
            _playerAnimator.SetWalk(_inputVector.x);
            _playerAnimator.SetJump(_grounded);
            _resetAttackTime -= Time.deltaTime;
            if (_resetAttackTime < 0 || !_grounded)
            {
                FinishAttack();
            }
        }
        private void FixedUpdate()
        {
            _grounded = Physics2D.OverlapCircle(_groundPoint.position, _groundCheckRadius, _groundLayers);
            _movement.Move(_inputVector.x * _speed);
            _movement.Jump(_jumping, _grounded,_jumpForce);
        }
        private void Init()
        {
            _directionForward = true;
            if(GameManager.LevelsManager.LevelsManager.playerInput != null)
            {
                _input = GameManager.LevelsManager.LevelsManager.playerInput;
                (_input as PlayerInput).SetInput(this);
            }
            else
            {
                _input = new PlayerInput(this, new PlayerControls());
                GameManager.LevelsManager.LevelsManager.playerInput = _input;
            }
            _movement = new PlayerMovement(_rigidbody2D,_asPlayer);
            _playerAnimator = new PlayerAnimator(_animator);
            _playerUse = GetComponentInChildren<PlayerUse>();
            _playerMeleeAttack = new PlayerMeleeAttack();
            _playerDistanceAttack = new PlayerDistanceAttack(_bulletPrefab, _amountBullet, _distanceDamage, transform);
            _scaleForward = transform.localScale;
            _scaleBackward = _scaleForward;
            _scaleBackward.x *= -1;
        }
        private void RequirementCheck()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _collider2D = gameObject.GetComponent<Collider2D>();
            _animator = gameObject.GetComponent<Animator>();
            if(_asPlayer == null)
            {
                _asPlayer = GetComponent<PlayerAudioSource>();
            }
        }
        public void SetJump(bool isJumping)
        {
            _jumping = isJumping;
        }
        public void SetInput(Vector2 inputVector, bool forward)
        {
            _inputVector = inputVector;
            Flip(forward);
        }
        public void Use(bool isUse)
        {
            _playerUse.Using();
        }
        private void Flip(bool direction)
        {
            transform.localScale = direction ? _scaleForward : _scaleBackward;
            _directionForward = direction;
        }
        public void Attack(string attackType)
        {
            if(!_isMeleeAttack && !_isDistanceAttack)
            {
                _resetAttackTime = _attackDelay;
                _playerAnimator.TryAttack();
                if (attackType == "leftButton" || attackType == "ctrl" && _grounded)
                {
                    _isMeleeAttack = true;
                    _playerAnimator.SetMeleeAttack();
                }
                if (attackType == "rightButton" || attackType == "rightShift")
                {
                    _isDistanceAttack = true;
                    _playerAnimator.SetDistanceAttack(_isDistanceAttack = _playerDistanceAttack.TrySetFlyKiss(GetComponent<PlayerStatus>()));
                }
            }
            // Можно попробовать вынести это все в отдельный скрипт атаки для сокращения объема скрипта плеера
            //Не логично статус впихивать в пространство GameUI оно ближе к своему собственному надо подумать
            //Дописать анимацию и реализовать атаку
        }
        public void InitBullet()
        {
            _playerDistanceAttack.InitFlyKiss(_spawnFlyKissPoint.position, _directionForward, _distanceDamage);
        }
        public void StartBullet()
        {
            _playerDistanceAttack.StartFlyKiss(_directionForward);
        }
        public void FinishAttack()
        {
            if (_isMeleeAttack)
            {
                _isMeleeAttack = false;
                _playerMeleeAttack.FinishMeleeAttack(_meleeDamage, gameObject.transform, _attackDirection, _meleeAttackDistance, _enemyLayers);
            }
            if (_isDistanceAttack)
            {
                _isDistanceAttack = false;
                _playerDistanceAttack.FinishDistanceAttack();
            }
        }
        public void LooseAllHP()
        {
            SetOff();
            _playerAnimator.LooseAllHP();
            GameObject.FindObjectOfType<GameManager.PlatformerManager>().PlayerLooseAllHPMsg();
            StartCoroutine(GameObject.FindObjectOfType<GameManager.LevelEffectManager>().PlayerHPRestartLevel());            
        }
        public void SetOff()
        {
            //_playerAnimator.Celebrate();
            _input.Untying();
            _collider2D.enabled = false;
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.Sleep();
        }
    }
}

