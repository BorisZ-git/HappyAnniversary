using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameControl
{
    public class GameController : AbstractGameController
    {
        private void Start()
        {
            Singletone();
            DontDestroyOnLoad(this);
        }
        private void Singletone()
        {
            if (SingletoneGameController._singletone != null) { Destroy(this.gameObject); }
            else
            {
                SingletoneGameController._singletone = this;
                _prefabsHashObj = Instantiate(_prefabsHashObj, transform);
                _prefabsHash = _prefabsHashObj.GetComponent<PrefabsHash>();
            }
        }
    }
}

