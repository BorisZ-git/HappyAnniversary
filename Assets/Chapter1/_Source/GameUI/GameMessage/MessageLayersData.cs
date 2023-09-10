using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer.MessageEventUI
{
    public class MessageLayersData : MonoBehaviour
    {
        [Header("Layers")]
        [SerializeField] private LayerMask _storeMask;
        [SerializeField] private LayerMask _buttonMask;
        [SerializeField] private LayerMask _exitMask;
        [SerializeField] private LayerMask _switcherMask;

        public LayerMask StoreMask { get => _storeMask; }
        public LayerMask ButtonMask { get => _buttonMask; }
        public LayerMask ExitMask { get => _exitMask; }
        public LayerMask SwitcherMask { get => _switcherMask; }

    }
}

