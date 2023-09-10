using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameUI
{
    public class UIController
    {
        private UIManager _uiMng;
        private UIControls _controlls;
        public void SetInput(UIManager uiManager)
        {
            _uiMng = uiManager;
            Bind();
        }
        public UIController(UIManager uiManager)
        {
            _uiMng = uiManager;
            _controlls = new UIControls();
            Bind();
        }
        public void Bind()
        {
            _controlls.UIControlls.Enable();
            _controlls.UIControlls.Esc.started += Esc;
        }
        public void Untying()
        {
            _controlls.UIControlls.Disable();
            _controlls.UIControlls.Esc.started -= Esc;
        }
        public void Esc(InputAction.CallbackContext context)
        {
            Debug.Log("Esc");
            _uiMng.EscPress();
        }
    }
}

