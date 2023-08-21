using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class PointRideEvent : PointsGeneral
{
    [Header("Pool Links")]
    [SerializeField] private PoolFallenObjs _poolFallObj;
    [SerializeField] private PointSpawnFallenObj[] _points;
    [Header("Spawn Values")]
    [SerializeField] private float _timeDelay;
    [Space(15)]
    [SerializeField] private bool _isQueue;

    private int _spawnIndex;
    private GameObject _spawnObject;
    private Collider2D _collider;
    //queue
    protected override void Awake()
    {
        base.Awake();
        if(_poolFallObj == null)
        {
            _poolFallObj = FindObjectOfType<PoolFallenObjs>();
        }
        _collider = GetComponent<Collider2D>();
        _spawnIndex = 0;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isQueue)
        {
            SpawnObjects();
        }
        else if (_isQueue)
        {
            SpawnObjectsQueue();
        }
        _collider.enabled = false;
    }
    IEnumerator SpawnDelayObjs()
    {
        _spawnIndex++;
        yield return new WaitForSeconds(_timeDelay);
        SpawnObjects();
    }
    private void SpawnObjects()
    {
        if(_spawnIndex >= _points.Length)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _spawnObject = CheckObject(_points[_spawnIndex]);
            _spawnObject.SetActive(true);
            StartCoroutine(SpawnDelayObjs());
        }
    }
    private void SpawnObjectsQueue()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            StartCoroutine(SpawnQueueObject(_points[i]));
        }
    }
    IEnumerator SpawnQueueObject(PointSpawnFallenObj point)
    {
        yield return new WaitForSeconds(point._spawnTimeDelay);
        _spawnObject = CheckObject(point);
        _spawnObject.SetActive(true);
    }
    private GameObject CheckEnemyStatus(GameObject enemy)
    {
        enemy.GetComponent<EnemyBehaviour>().rb2D.velocity = Vector2.zero;
        if (enemy.GetComponent<EnemyBehaviour>().IsDead)
        {
            return CheckEnemyStatus(_poolFallObj.EnemyPool.GetNextInactiveObject(enemy));
        }
        return enemy;
    }
   private GameObject CheckObject(PointSpawnFallenObj point)
    {
        if (point._isEnemy)
        {
            _spawnObject = CheckEnemyStatus(_poolFallObj.GetInactiveEnemy());
            _spawnObject.transform.position = point.transform.position;
        }
        else
        {
            _spawnObject = _poolFallObj.GetInactiveFallenObj();
            _spawnObject.GetComponent<FallObject>().ActivateObject(point.transform.position);
        }
        return _spawnObject;
    }
}
