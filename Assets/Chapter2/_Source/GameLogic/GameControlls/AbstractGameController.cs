using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameControl
{
    /// <summary>
    /// Contain fields and realize live logic GameController: 
    /// Pattern Singltone; 
    /// </summary>
    public abstract class AbstractGameController : MonoBehaviour
    {
        [SerializeField] private GameObject _prefabsHashObj;
        private LinksHash _links;
        protected PrefabsHash _prefabsHash;
        virtual public void Awake()
        {
            Singletone();
            DontDestroyOnLoad(this);
        }
        protected void Singletone()
        {
            if (SingletoneGameController._singletone != null) { Destroy(this.gameObject); }
            else
            {
                SingletoneGameController._singletone = this;
                _prefabsHashObj = Instantiate(_prefabsHashObj, transform);
                _prefabsHash = _prefabsHashObj.GetComponent<PrefabsHash>();
                _links = new LinksHash();
            }
        }
    }
}

