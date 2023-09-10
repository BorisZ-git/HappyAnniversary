using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;

public class PoolFallenObjs : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _fallenBox;
    [SerializeField] private GameObject _fallenBarrel;
    [SerializeField] private GameObject _enemy;
    [Header("Count Values")]
    [SerializeField] private int _boxValue;
    [SerializeField] private int _barellValue;
    [SerializeField] private int _enemyValue;
    [Header("PoolParent")]
    [SerializeField] private GameObject _boxPoolParent;
    [SerializeField] private GameObject _barellPoolParent;
    [SerializeField] private GameObject _enemyPoolParent;
    private ObjectPool _fallBoxPool;
    private ObjectPool _fallBarellPool;
    private ObjectPool _enemyPool;
    public ObjectPool FallBoxPool { get => _fallBoxPool; }
    public ObjectPool FallBarellPool { get => _fallBarellPool; }
    public ObjectPool EnemyPool { get => _enemyPool; }

    private void Awake()
    {
        if (_boxPoolParent == null)
        {
            _boxPoolParent = new GameObject("BoxPool");
            _boxPoolParent.transform.SetParent(transform);

        }
        if (_barellPoolParent == null)
        {
            _barellPoolParent = new GameObject("BarellPool");
            _barellPoolParent.transform.SetParent(transform);
        }
        if(_enemyPoolParent == null)
        {
            _enemyPoolParent = new GameObject("EnemyPool");
            _enemyPoolParent.transform.SetParent(transform);
        }
        if (_fallenBox)
        {
            _fallBoxPool = new ObjectPool(_fallenBox, _boxValue, _boxPoolParent.transform);
        }
        else
        {
            print("Not set box prefab");
        }
        if (_fallenBarrel)
        {
            _fallBarellPool = new ObjectPool(_fallenBarrel, _barellValue, _barellPoolParent.transform);
        }
        else
        {
            print("Not set barell prefab");
        }
        if (_enemy)
        {
            _enemyPool = new ObjectPool(_enemy, _enemyValue, _enemyPoolParent.transform);
        }
        else
        {
            print("Not set enemy prefab");
        }
    }
    public GameObject GetInactiveFallenObj()
    {
        if (Random.Range(0, 11) >= 6)
        {
            return _fallBoxPool.GetInactiveObject();
        }
        else
        {
            return _fallBarellPool.GetInactiveObject();
        }
    }
    public GameObject GetInactiveEnemy()
    {
        return _enemyPool.GetInactiveObject();
    }
}
