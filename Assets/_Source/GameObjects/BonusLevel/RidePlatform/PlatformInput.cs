using UnityEngine;
using UnityEngine.InputSystem;

namespace BonusLevel.RidePlatform
{
    public class PlatformInput : InputParent
    {
        private RidePlatformGeneral _ridePlatform;
        public void SetInput(RidePlatformGeneral ridePlatform)
        {
            _ridePlatform = ridePlatform;
        }
        public PlatformInput(RidePlatformGeneral ridePlatform, IInputActionCollection2 controls) : base(controls)
        {
            _ridePlatform = ridePlatform;
        }
        public override void Bind()
        {
            (_controls as RidePlatformControls).RidePlatform.Enable();
            (_controls as RidePlatformControls).RidePlatform.Move.started += OnMoveIn;
            (_controls as RidePlatformControls).RidePlatform.Move.canceled += OnMoveIn;
            (_controls as RidePlatformControls).RidePlatform.Use.started += OnUseIn;

        }
        public override void Untying()
        {
            (_controls as RidePlatformControls).RidePlatform.Disable();
            (_controls as RidePlatformControls).RidePlatform.Move.started -= OnMoveIn;
            (_controls as RidePlatformControls).RidePlatform.Move.canceled -= OnMoveIn;
            (_controls as RidePlatformControls).RidePlatform.Use.started -= OnUseIn;

        }
        public void OnMoveIn(InputAction.CallbackContext context)
        {
            _ridePlatform.SetInput(context.ReadValue<Vector2>());
            _ridePlatform.AudioMove(context.ReadValue<Vector2>());
        }
        public void OnUseIn(InputAction.CallbackContext context)
        {
            _ridePlatform.Use();
        }
    }
}

