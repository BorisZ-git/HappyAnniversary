using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]

abstract public class PointsGeneral : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] protected BonusLevelManager _bonusLvlManager;
    [SerializeField] protected LayerMask _playerLayer;
    protected virtual void Awake()
    {
        if (_bonusLvlManager == null)
        {
            _bonusLvlManager = FindObjectOfType<BonusLevelManager>();
        }
    }
    abstract protected void OnTriggerEnter2D(Collider2D collision);
}
