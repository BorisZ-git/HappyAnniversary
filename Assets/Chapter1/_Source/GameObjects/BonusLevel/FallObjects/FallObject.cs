using GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;

public class FallObject : ObstacleObjects
{
    [Header("Physics Value")]
    [SerializeField] private float _crushForce;
    private bool _isDmgDone;
    private float _rotateAngle;
    private void Start()
    {
        _rotateAngle = Random.Range(0, 11);
        if (_rotateAngle >= 6)
        {
            _rotateAngle = Random.Range(1, 5);
        }
        else
        {
            _rotateAngle = -Random.Range(1, 5); ;
        }
    }

    private void FixedUpdate()
    {
        if (!_isDmgDone)
        {
            transform.Rotate(new Vector3(0, 0, _rotateAngle), Space.Self);
        }
    }
    public void ActivateObject(Vector2 spawnPoint)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = spawnPoint;
        _collider.enabled = true;
        _animatorObj.SetBool(_hashAnimBoolName, true);
    }
    private void DisableObject()
    {
        _animatorObj.SetBool(_hashAnimBoolCrush, false);
        _isDmgDone = false;
        gameObject.SetActive(false);
    }

    protected override void Crush(Collider2D collision)
    {
        if (!_isDmgDone)
        {
            _rgbd.AddForce(Vector2.up * _crushForce,ForceMode2D.Impulse);
            if (!string.IsNullOrEmpty(_hashAnimCrush))
            {
                _animatorObj.Play(_hashAnimCrush);
            }
            if (Utils.IsInLayer(collision.gameObject.layer, _player))
            {
                collision.GetComponent<Status>().SetHP(-_damage);
            }
            else if(Utils.IsInLayer(collision.gameObject.layer, _enemy))
            {
                collision.GetComponent<Status>().SetHP(-_damage*3);
            }
            StartCoroutine(CrushCount());
            _isDmgDone = true;
        }
    }
    IEnumerator CrushCount()
    {        
        _collider.enabled = false;
        _animatorObj.SetBool(_hashAnimBoolCrush, true);
        yield return new WaitForSeconds(_timeDmgDone);
        DisableObject();
    }
}
