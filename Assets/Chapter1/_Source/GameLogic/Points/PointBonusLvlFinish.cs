using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
using GameManager.LevelsManager;

public class PointBonusLvlFinish : PointsGeneral
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayer(collision.gameObject.layer, _playerLayer))
        {
            LevelsManager.FinishScene();
        }
    }
}
