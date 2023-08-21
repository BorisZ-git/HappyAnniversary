using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.MessageEventUI
{
    public class TalkerSpritesData : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite _talkerAlice;
        [SerializeField] private Sprite _talkerBoris;
        [SerializeField] private Sprite _anotherTalker;
        public Sprite TalkerAlice { get => _talkerAlice; }
        public Sprite TalkerBoris { get => _talkerBoris; }
        public Sprite AnotherTalker { get => _anotherTalker; }

        private void Awake()
        {
            if(_talkerAlice == null)
            {
                _talkerAlice = FindObjectOfType<Player.Player>().GetComponentInChildren<SpriteRenderer>().sprite;
            }
        }
    }
}

