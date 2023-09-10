using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;

public class Obstacle : ObstacleObjects
{
    [Header("Crush Values")]
    [SerializeField] private float _forceCrush;
    private GameObject _enemyCrush;
    private bool _isPlayerDmgDone;
    protected override void Crush(Collider2D collision)
    {
        _animatorObj.Play(_hashAnimCrush);
        if (Utils.IsInLayer(collision.gameObject.layer, _player))
        {
            if (!_isPlayerDmgDone)
            {
                collision.GetComponent<GameUI.Status>().SetHP(-_damage);
                StartCoroutine(CountDmgDone());
            }
        }
        if(Utils.IsInLayer(collision.gameObject.layer, _enemy) && collision.gameObject != _enemyCrush)
        {
            StopCoroutine(CountEnemyDmgDone());
            collision.GetComponent<GameUI.Status>().SetHP(-_damage);
            collision.transform.SetParent(null);
            collision.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * _forceCrush, ForceMode2D.Impulse);
            _enemyCrush = collision.gameObject;
        }
    }
    IEnumerator CountDmgDone()
    {
        _isPlayerDmgDone = true;
        yield return new WaitForSeconds(_timeDmgDone);
        _isPlayerDmgDone = false;
    }
    IEnumerator CountEnemyDmgDone()
    {
        yield return new WaitForSeconds(_timeDmgDone);
        _enemyCrush = null;
    }
}
