using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Supporting;
namespace InteractibleObj
{
    [RequireComponent(typeof(Collider2D))]
    public class ButtonFallObjs : ButtonActivateObject
    {
        [Header("Prefab")]
        [SerializeField] private GameObject _fallObjPrefab;
        [Header("Points")]
        [SerializeField] private PointSpawnFallenObj[] _points;
        private ObjectPool _pool;
        private GameObject _spawnObj;
        private Button _button;
        private void Awake()
        {
            if (_fallObjPrefab)
            {
                _pool = new ObjectPool(_fallObjPrefab, _points.Length, this.transform);
            }
            _button = GetComponentInChildren<Button>();
        }
        public bool Use()
        {
            if (!base.IsActiveted && _fallObjPrefab)
            {
                _button.PushButton();
                Spawn();
                return true;
            }
            else
            {
                print("some sound is not ready");
                return false;
            }
        }
        private void Spawn()
        {
            if(_pool != null && _points.Length > 0)
            {
                foreach (var item in _points)
                {
                    _spawnObj = _pool.GetInactiveObject();
                    _spawnObj.GetComponent<FallObject>().ActivateObject(item.transform.position);
                }
            }
            else
            {
                print("_pool don't exist or not set points");
            }
        }
    }
}

