using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Player
{
    public class PlayerAnimator
    {
        private Animator _animator;
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _groundedHash = Animator.StringToHash("Grounded");
        private readonly int _hashAttackTransit = Animator.StringToHash("Attack");
        private readonly int _meleeAttack = Animator.StringToHash("MeleeAttack");
        private readonly int _distanceAttack = Animator.StringToHash("DistanceAttack");
        private readonly int _looseAllHP = Animator.StringToHash("LooseAllHP");
        public PlayerAnimator(Animator animator)
        {
            _animator = animator;
        }
        public void SetWalk(float inputX)
        {
            _animator.SetFloat(_speedHash, Mathf.Abs(inputX));
        }
        public void SetJump(bool grounded)
        {
            _animator.SetBool(_groundedHash, grounded);
        }
        public void TryAttack()
        {
            _animator.SetTrigger(_hashAttackTransit);
        }
        public void SetMeleeAttack()
        {
            _animator.SetTrigger(_meleeAttack);
        }
        public void SetDistanceAttack(bool value)
        {
            if (value)
            {
                _animator.SetTrigger(_distanceAttack);
            }
        }
        public void LooseAllHP()
        {
            _animator.SetTrigger(_looseAllHP);
        }
        public void Celebrate()
        {

        }
    }
}

