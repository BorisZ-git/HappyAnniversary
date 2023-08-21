using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
[RequireComponent(typeof(DrawArea))]
[RequireComponent(typeof(AudioSource))]
public class SpawnPrefabInArea : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private int[] _prefabCounts;
    [Header("Spawn Properties")]
    [SerializeField] private float _timeSpawnDelay;
    [SerializeField] private AudioSource _audioSource;

    private DrawArea _area;
    private Vector2 _spawnPoint;
    private float _timeCount;
    private List<ObjectPool> _pools;
    private bool _check;
    private GameObject _spawnedObject;
    private int _index;
    private void Awake()
    {
        if(_prefabs != null && _prefabs.Length != 0)
        {
            if(_prefabCounts != null && _prefabCounts.Length != 0)
            {
                _pools = new List<ObjectPool>();
                _area = GetComponent<DrawArea>();
                CreatePoolsObject();
                _check = true;
            }
        }
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        _timeCount += Time.deltaTime;
        if(_timeCount >= _timeSpawnDelay)
        {
            if (_check)
            {
                Spawn();
            }
            else
            {
                print("Check is false, something wrong");
            }
            _timeCount = 0;
        }
    }
    private void Spawn()
    {
        CheckSceneForActiveObject();
        GetSpawnPoint();
        GetSpawnedObject();
        _spawnedObject.SetActive(true);
        _spawnedObject.transform.position = _spawnPoint;
        _audioSource.Play();
    }
    private void GetSpawnedObject()
    {
        _index = Random.Range(0, 101);
        _spawnedObject = _pools[_index < 39 ? 0 : 1].GetInactiveObject();
    }
    private void GetSpawnPoint()
    {
        _spawnPoint.x = Random.Range(_area.AreaX[0], _area.AreaX[1]);
        _spawnPoint.y = Random.Range(_area.AreaY[0], _area.AreaY[1]);
    }
    private void CreatePoolsObject()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            if (i >= _prefabCounts.Length)
            {
                _pools.Add(new ObjectPool(_prefabs[i], _prefabCounts[_prefabCounts.Length - 1], this.transform));
            }
            else
            {
                _pools.Add(new ObjectPool(_prefabs[i], _prefabCounts[i], this.transform));
            }
        }
    }
    private void CheckSceneForActiveObject()
    {
        if (_spawnedObject != null && _spawnedObject.gameObject.activeInHierarchy)
        {
            _spawnedObject.SetActive(false);
        }
    }
}
