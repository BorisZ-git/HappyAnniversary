using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Supporting;
using GameUI;
namespace Enemy
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyAttack : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private int _damage;
        [Header("Links")]
        [SerializeField] private EnemyBehaviour _behaviour;
        [Header("Layers")]
        [SerializeField] private LayerMask _player;
        [Header("Prefabs")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _fallObjsPrefab;
        [Header("Spawn Points")]
        [SerializeField] private PointSpawnFallenObj[] _points;

        private Status _targetStatus;
        private float _attackTime;

        private void Start()
        {
            if(_behaviour == null)
            {
                _behaviour = GetComponentInParent<EnemyBehaviour>();
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _player))
            {
                _targetStatus = collision.gameObject.GetComponent<Status>();
                _behaviour.CheckDirection(_targetStatus.transform.position.x < transform.position.x ? false : true);
                if (!_behaviour.IsAttack && !_behaviour.IsDead)
                {
                    StartCoroutine(Attack());
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _player))
            {
                _targetStatus = null;
            }
        }
        IEnumerator Attack()
        {
            _behaviour.IsAttack = true;
            _behaviour.EnemyAnim.Attack(_behaviour.IsAttack);
            _behaviour.EnemyAnim.Walk(false);
            _attackTime = _behaviour.EnemyAnim.Anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(_attackTime);
            SetDamage();
        }

        public void SetDamage()
        {
            StopCoroutine(Attack());
            if (_targetStatus != null)
            {
                _targetStatus.SetHP(-_damage);
                StartCoroutine(Attack());
            }
            else if(_targetStatus == null)
            {
                _behaviour.IsAttack = false;
                _behaviour.EnemyAnim.Attack(_behaviour.IsAttack);
            }
        }
        public void Dead()
        {
            StopAllCoroutines();
            this.enabled = false;
        }
    }
}

