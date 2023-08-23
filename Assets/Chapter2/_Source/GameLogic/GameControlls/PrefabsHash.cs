using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameControl
{
    /// <summary>
    /// Contains links on game prefabs for short call instantiate
    /// </summary>
    public class PrefabsHash : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _enemy;
        [SerializeField] private GameObject _bonusHP;
        [SerializeField] private GameObject _bonusMap;
        [SerializeField] private GameObject _bonusMP;

        public GameObject Player { get => _player; }
        public GameObject Enemy { get => _enemy; }
        public GameObject BonusHP { get => _bonusHP; }
        public GameObject BonusMap { get => _bonusMap; }
        public GameObject BonusMP { get => _bonusMP; }
    }
}

