using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement 
{
    private Transform _level;
    private float _hashSpeed;
    private float _moveFreeze;
    private float _speed;
    public LevelMovement(Transform level, float speed, float moveFreeze)
    {
        _level = level;
        _hashSpeed = speed;
        _moveFreeze = moveFreeze;
        _speed = _hashSpeed;
    }
    public void LevelMove()
    {
        _level.Translate(Vector2.left * Time.deltaTime * _speed);
    }
    public void DecreseSpeed()
    {
        if (_speed > 0)
        {
            _speed -= _moveFreeze * Time.deltaTime;
        }
        else
        {
            _speed = 0;
        }
    }
    public bool IncreaseSpeed()
    {
        if (_speed < _hashSpeed)
        {
            _speed += _moveFreeze * Time.deltaTime;
            return true;
        }
        else
        {
            _speed = _hashSpeed;
            return false;
        }
    }
}
