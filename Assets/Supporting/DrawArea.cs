using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArea : MonoBehaviour
{
    [SerializeField] private bool _isDrawFromObject;
    [Header("Edges Value")]
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _upLimit;
    [SerializeField] private float _bottomLimit;
    [SerializeField] private Color _edgeColor;
    private float _left, _right, _up, _bottom;
    /// <summary>
    /// get 0 = left, 1 = right
    /// </summary>
    public float[] AreaX {get => new float[] {_left,_right }; }
    /// <summary>
    /// get 0 = bottom, 1 = up
    /// </summary>
    public float[] AreaY { get => new float[] { _bottom, _up}; }
    private void Awake()
    {
        SetLineValues();
    }
    private void SetLineValues()
    {
        if (_isDrawFromObject)
        {
            _left = _leftLimit + transform.position.x;
            _right = _rightLimit + transform.position.x;
            _up = _upLimit + transform.position.y;
            _bottom = _bottomLimit + transform.position.y;
        }
        else
        {
            _left = _leftLimit;
            _right = _rightLimit;
            _up = _upLimit;
            _bottom = _bottomLimit;
        }
    }

    private void OnDrawGizmos()
    {
        SetLineValues();
        Gizmos.color = _edgeColor;
        // from left to right up -
        Gizmos.DrawLine(new Vector3(_left, _up), new Vector3(_right, _up));
        // from left to right bottom _
        Gizmos.DrawLine(new Vector3(_left, _bottom), new Vector3(_right, _bottom));
        // from up to bottom left |
        Gizmos.DrawLine(new Vector3(_left, _up), new Vector3(_left, _bottom));
        // from up to bottom right |
        Gizmos.DrawLine(new Vector3(_right, _up), new Vector3(_right, _bottom));
    }
}
