using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAnimation
{
    private bool _isHurt, _isRed;
    private float _hurtSpeed;
    private SpriteRenderer _spriteRender;
    public HurtAnimation(SpriteRenderer spriteRenderer, float hurtSpeed)
    {
        _spriteRender = spriteRenderer;
        _hurtSpeed = hurtSpeed;
    }

    public bool ResetHurtAnim()
    {
        _spriteRender.color = Color.white;
        _isRed = false;
        return _isHurt = true;
    }
    public bool HurtAnim()
    {
        if (!_isRed)
        {
            _spriteRender.color =
                new Color(Mathf.Lerp(_spriteRender.color.r, Color.red.r, _hurtSpeed),
                Mathf.Lerp(_spriteRender.color.g, Color.red.g, _hurtSpeed),
                Mathf.Lerp(_spriteRender.color.b, Color.red.b, _hurtSpeed));
            if (_spriteRender.color == Color.red)
            {
                _isRed = true;
            }
        }
        else
        {
            _spriteRender.color =
                new Color(Mathf.Lerp(_spriteRender.color.r, Color.white.r, _hurtSpeed * Time.deltaTime),
                Mathf.Lerp(_spriteRender.color.g, Color.white.g, _hurtSpeed * Time.deltaTime),
                Mathf.Lerp(_spriteRender.color.b, Color.white.b, _hurtSpeed * Time.deltaTime));
            if (_spriteRender.color == Color.white)
            {
                _isRed = false;
                _isHurt = false;
            }
        }
        return _isHurt;
    }
}
