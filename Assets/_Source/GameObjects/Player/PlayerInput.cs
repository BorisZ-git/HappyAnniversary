using UnityEngine;
using UnityEngine.InputSystem;

namespace Player 
{
    public class PlayerInput : InputParent
    {
        private Player _player;
        private bool _forward;
        private Vector2 _inputVector;
        public void SetInput(Player player)
        {
            _player = player;
            Bind();
        }
        public PlayerInput(Player player, IInputActionCollection2 controls) : base(controls)
        {
            _player = player;
            Bind();
        }
        public override void Bind()
        {
            (_controls as PlayerControls).Player.Enable();
            (_controls as PlayerControls).Player.Move.started += OnMoveIn;
            (_controls as PlayerControls).Player.Move.canceled += OnMoveIn;
            (_controls as PlayerControls).Player.Jump.started += OnJumpIn;
            (_controls as PlayerControls).Player.Jump.canceled += OnJumpIn;
            (_controls as PlayerControls).Player.Use.started += OnUseIn;
            (_controls as PlayerControls).Player.Attack.started += OnAttackIn;

        }
        public override void Untying()
        {
            (_controls as PlayerControls).Player.Disable();
            (_controls as PlayerControls).Player.Move.started -= OnMoveIn;
            (_controls as PlayerControls).Player.Move.canceled -= OnMoveIn;
            (_controls as PlayerControls).Player.Jump.started -= OnJumpIn;
            (_controls as PlayerControls).Player.Jump.canceled -= OnJumpIn;
            (_controls as PlayerControls).Player.Use.started -= OnUseIn;
            (_controls as PlayerControls).Player.Attack.started -= OnAttackIn;
        }
        private void OnAttackIn(InputAction.CallbackContext context)
        {
            _player.Attack(context.control.name);            
        }

        private void OnMoveIn(InputAction.CallbackContext context)
        {
            _inputVector = context.ReadValue<Vector2>();
            if (_inputVector.x > 0) _forward = true;
            else if (_inputVector.x < 0) _forward = false;
            _player.SetInput(_inputVector, _forward);
        }
        private void OnJumpIn(InputAction.CallbackContext context)
        {
            _player.SetJump(context.ReadValueAsButton());
        }
        private void OnUseIn(InputAction.CallbackContext context)
        {
            _player.Use(context.ReadValueAsButton());
        }
    }
}   

