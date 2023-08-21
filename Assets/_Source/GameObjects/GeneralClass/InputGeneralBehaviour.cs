using UnityEngine;

abstract public class InputGeneralBehaviour : MonoBehaviour
{
    [Header("Game Property")]
    [SerializeField] protected float _speed;
    protected InputParent _input;
    protected Vector2 _vectorInput;
    public void SetActive(bool value)
    {
        if (value)
        {
            _input.Bind();
        }
        else
        {
            _input.Untying();
        }
    }
    public void SetInput(Vector2 vector2)
    {
        _vectorInput = vector2;
    }
}
