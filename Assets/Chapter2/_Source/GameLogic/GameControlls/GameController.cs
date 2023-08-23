using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameControl
{
    public class GameController : AbstractGameController
    {
        // Restruct Singltone
        // Need global links for object like player
        // Need check for exclusive object
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
        /// <summary>
        /// On start call for another class method that fills scene with enemy player and other objects
        /// </summary>
        private void FillScene()
        {
            Instantiate(_prefabsHash.Enemy);
        }
    }
}

