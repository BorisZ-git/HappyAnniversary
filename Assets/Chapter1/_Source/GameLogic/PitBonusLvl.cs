using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
using GameManager.LevelsManager;
[RequireComponent(typeof(BoxCollider2D))]
public class PitBonusLvl : MonoBehaviour
{
    public LayerMask _playerMask;
    public LayerMask _disableObjsMask;
    public LayerMask _enemyLayer;
    private GameObject _touchedObj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _touchedObj = collision.gameObject;
        if (Utils.IsInLayer(_touchedObj.layer, _playerMask))
        {
            LevelsManager.RestartLevel();
        }
        if(Utils.IsInLayer(_touchedObj.layer, _disableObjsMask))
        {
            if (Utils.IsInLayer(_touchedObj.layer, _enemyLayer))
            {
                _touchedObj.GetComponent<GameUI.Status>().Hp = 3;
                _touchedObj.GetComponent<GameUI.Status>().SetHP(0);
            }
            _touchedObj.SetActive(false);
        }
    }
}
