using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
public class PointEnterPlayerRegime : PointsGeneral
{
    [Header("PointToRelocatePlatform")]
    [SerializeField] private Transform _pointToSwapPlatform;
    [SerializeField] private Transform _pointToStablePlatform;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _playerLayer))
        {
            _bonusLvlManager.LevelStopMove(_pointToSwapPlatform, _pointToStablePlatform);
        }
        this.enabled = false;
    }
}
