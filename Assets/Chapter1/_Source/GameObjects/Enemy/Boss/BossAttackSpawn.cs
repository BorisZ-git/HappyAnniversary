using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSpawn
{
    private PoolFallenObjs _poolFallObj;
    private PointSpawnFallenObj[] _points;
    private GameObject _spawnObj;

    public BossAttackSpawn(PoolFallenObjs poolFallObj, PointSpawnFallenObj[] points)
    {
        _poolFallObj = poolFallObj;
        _points = points;
    }
    
    public void SpawnObj()
    {
        foreach (var item in _points)
        {
            if (!item._isEnemy)
            {
                _spawnObj = _poolFallObj.GetInactiveFallenObj();
                _spawnObj.GetComponent<FallObject>().ActivateObject(item.transform.position);
            }
        }
    }
    public void SpawnEnemy()
    {
        foreach (var item in _points)
        {
            if (item._isEnemy)
            {
                _spawnObj = _poolFallObj.GetInactiveEnemy();
                while (_spawnObj.GetComponent<Enemy.EnemyBehaviour>().IsDead)
                {
                    _spawnObj = _poolFallObj.EnemyPool.GetNextInactiveObject(_spawnObj);
                }
                _spawnObj.transform.position = item.transform.position;
                _spawnObj.SetActive(true);
            }
        }
    }

}
