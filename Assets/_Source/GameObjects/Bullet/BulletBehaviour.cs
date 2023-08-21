using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Animator))]
    public class BulletBehaviour : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        [Header("Layers")]
        [SerializeField] private LayerMask _damagebleLayer;
        [SerializeField] private LayerMask _enemyLayer;

        private Rigidbody2D _rigidbody2D;
        private int _damage;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        public void Init(Vector2 spawnPosition, bool directionForward, int damage)
        {
            gameObject.SetActive(true);
            _damage = damage;
            transform.position = spawnPosition;
            transform.rotation = directionForward ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        }
        public void Blow(bool directionForward)
        {
            ApplyForce(directionForward);
            StartCoroutine(CountLifeTime());
        }
        private void ApplyForce(bool directionForward)
        {
            transform.SetParent(null);
            _rigidbody2D.velocity = directionForward ? Vector2.right * _speed : Vector2.left * _speed;
        }
        private void DeactivateBullet()
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Utils.IsInLayer(collision.gameObject.layer, _enemyLayer))
            {
                collision.GetComponent<GameUI.Status>().SetHP(-_damage);
                DeactivateBullet();
            }
            if (Utils.IsInLayer(collision.gameObject.layer, _damagebleLayer))
            {
                DeactivateBullet();
            }
        }
        IEnumerator CountLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            DeactivateBullet();
        }
    }
}

