using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Supporting
{
    public class ObjectPool
    {
        private List<GameObject> _pool;
        private GameObject _prefab;
        private Transform _parent;
        public ObjectPool(GameObject prefab, int objectsCount, Transform parent)
        {
            _pool = new List<GameObject>();
            _prefab = prefab;
            _parent = parent;
            for (int i = 0; i < objectsCount; i++)
            {
                _pool.Add(GameObject.Instantiate(prefab, parent));
                _pool[i].SetActive(false);
            }
        }
        public GameObject GetInactiveObject()
        {
            foreach (var item in _pool)
            {
                if (!item.activeInHierarchy)
                {
                    return item;
                }
            }
            return PoolInactiveObjEmpty();
        }
        public GameObject GetNextInactiveObject(GameObject wrongObj)
        {
            int index = _pool.IndexOf(wrongObj);
            if (_pool.Count-1 < index)
            {
                for (int i = ++index; i < _pool.Count - 1; i++)
                {
                    if (!_pool[i].activeInHierarchy)
                    {
                        return _pool[i];
                    }
                }
            }
            return PoolInactiveObjEmpty();            
        }
        public GameObject PoolInactiveObjEmpty()
        {
            _pool.Add(GameObject.Instantiate(_prefab, _parent));
            _pool[_pool.Count-1].SetActive(false);
            return _pool[_pool.Count-1];
        }
    }
}

