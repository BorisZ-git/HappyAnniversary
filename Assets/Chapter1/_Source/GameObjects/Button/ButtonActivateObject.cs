using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InteractibleObj
{
    public abstract class ButtonActivateObject : MonoBehaviour
    {
        private bool _isActiveted;

        public bool IsActiveted { get => _isActiveted; set => _isActiveted = value; }

    }
}

