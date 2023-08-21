using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [Header("Hash Animator Params")]
        [SerializeField] private string _hashIsDeadAnim;
        [SerializeField] private string _hashIsWalkAnim;
        [SerializeField] private string _hashAttackAnim;
        [Header("Values")]
        [SerializeField] private float _hurtSpeed;
        [Header("Links")]
        [SerializeField] private Animator _animator;
        private bool _isHurt;
        private HurtAnimation _hurtAnim;
        public Animator Anim { get => _animator; }
        public SpriteRenderer Sprite { get; set; }
        private void Start()
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            _hurtAnim = new HurtAnimation(Sprite, _hurtSpeed);
        }
        private void Update()
        {
            if (_isHurt)
            {
                _isHurt = _hurtAnim.HurtAnim();
            }
        }
        public void Walk(bool IsWalk)
        {
            _animator.SetBool(_hashIsWalkAnim, IsWalk);
        }
        public void Attack(bool IsAttack)
        {
            _animator.SetBool(_hashAttackAnim, IsAttack);
        }
        public void Hurted()
        {
            _isHurt = _hurtAnim.ResetHurtAnim();
        }
        public void Dead(bool IsDead)
        {
            Sprite.color = Color.white;
            Walk(false);
            Attack(false);
            _animator.SetBool(_hashIsDeadAnim, IsDead);
        }
    }
}

