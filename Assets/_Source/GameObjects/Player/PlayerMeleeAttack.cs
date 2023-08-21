using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
namespace Player
{
    public class PlayerMeleeAttack
    {
        private RaycastHit2D _hit;
        private GameUI.Status _targetStatus;
        private int _damage;
        private Transform _playerPos;
        private Transform _attackDirection;
        private float _attackDistance;
        private LayerMask _enemyLayers;

        public void SetDamage()
        {
            _hit = Physics2D.Raycast(_playerPos.position, _attackDirection.position - _playerPos.position, _attackDistance, _enemyLayers);
            if (_hit.collider != null)
            {
                _targetStatus = _hit.collider.gameObject.GetComponent<GameUI.Status>();
                _targetStatus.SetHP(-_damage);
            }
        }
        public void FinishMeleeAttack(int damage, Transform playerPos, Transform attackDirection, float attackDistance, LayerMask enemyLayers)
        {
            _damage = damage;
            _playerPos = playerPos;
            _attackDirection = attackDirection;
            _attackDistance = attackDistance;
            _enemyLayers = enemyLayers;
            SetDamage();
        }
    }
}

