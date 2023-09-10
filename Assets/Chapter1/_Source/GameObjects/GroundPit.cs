using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GroundPit : MonoBehaviour
{
    [Header("Layers")]
    [SerializeField] private LayerMask _playerLayer;

    private void FallInPit()
    {
        GameManager.LevelsManager.LevelsManager.RestartLevel();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Supporting.Utils.IsInLayer(collision.gameObject.layer, _playerLayer))
        {
            FallInPit();
        }
    }
}
