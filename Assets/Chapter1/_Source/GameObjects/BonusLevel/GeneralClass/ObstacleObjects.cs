using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
abstract public class ObstacleObjects : MonoBehaviour
{
    [SerializeField] protected LayerMask _damageLayer;
    [SerializeField] protected LayerMask _player;
    [SerializeField] protected LayerMask _enemy;
    [SerializeField] protected int _damage;
    [SerializeField] protected string _hashAnimCrush;
    [SerializeField] protected string _hashAnimBoolName;
    [SerializeField] protected string _hashAnimBoolCrush;
    [SerializeField] protected float _timeDmgDone;
    protected Animator _animatorObj;
    protected Collider2D _collider;
    protected Rigidbody2D _rgbd;
    protected AudioSource _audio;

    private void Awake()
    {
        _animatorObj = GetComponent<Animator>();        
        _collider = GetComponent<Collider2D>();
        _rgbd = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _damageLayer))
        {
            Crush(collision);
            _audio.Play();
        }
    }
    abstract protected void Crush(Collider2D collision);
}
