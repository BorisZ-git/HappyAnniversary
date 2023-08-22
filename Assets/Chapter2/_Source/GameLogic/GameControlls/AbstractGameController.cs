using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameControl
{
    public abstract class AbstractGameController : MonoBehaviour
    {
        [SerializeField] protected GameObject _prefabsHashObj;
        protected PrefabsHash _prefabsHash;
    }
}

