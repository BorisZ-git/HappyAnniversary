using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private Rigidbody2D _rigidbody2D;
    private PlayerAudioSource _asPlayer;
    public PlayerMovement(Rigidbody2D rigidbody2D, PlayerAudioSource asPlayer)
    {
        _rigidbody2D = rigidbody2D;
        _asPlayer = asPlayer;
    }
    public void Move(float inputX)
    {
        _rigidbody2D.velocity = new Vector2(inputX, _rigidbody2D.velocity.y);
    }

    public void Jump(bool jumping, bool grounded, float force)
    {
        if(jumping && grounded)
        {
            // Reset apllying force
            _asPlayer.AudioJump();
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
    }
}
