using UnityEngine.InputSystem;

abstract public class InputParent
{
    protected IInputActionCollection2 _controls;
    public InputParent(IInputActionCollection2 controls)
    {
        _controls = controls;
    }
    public abstract void Bind();
    public abstract void Untying();

}
