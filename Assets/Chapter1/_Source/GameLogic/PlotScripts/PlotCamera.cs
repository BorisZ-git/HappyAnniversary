using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlotCamera : MonoBehaviour
{
    [Header("Test Values")]
    [SerializeField] private float _testValueZoom;
    [SerializeField] private float _testEffectValue;

    [Header("Preset Value")]
    [SerializeField] private float _ortographicSize;
    [Header("Effects Value")]
    [SerializeField] private int _zoomEffectCount;
    [SerializeField] private float _zoomEffectSpeed;

    [Header("Links")]
    [SerializeField] private Camera _camera;
    private bool _test;
    private bool _tmp;
    private bool _isZoomEffect;
    private int _zoomEffectCheck;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }
    void Start()
    {
        StartCoroutine(CheckEffect());
    }
    void Update()
    {
        if (_test)
        {
            ZoomEffect(_testEffectValue);
            if(_zoomEffectCheck == 0)
            {
                _test = false;
                _isZoomEffect = false;
                Debug.Log("Test finish");
            }
        }
    }
    private void ZoomEffect(float effectValue)
    {
        _isZoomEffect = true;
        if (!_tmp)
        {
            ZoomOn(effectValue);
        }
        else if (_tmp)
        {
            ZoomOff();
        }

    }
    private void ZoomOn(float ortographicSize)
    {
        if(Mathf.Round(_camera.orthographicSize) == Mathf.Round(ortographicSize))
        {
            _tmp = true;
        }
        else
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, ortographicSize, _zoomEffectSpeed);
        }
    }
    private void ZoomOff()
    {
        if(Mathf.Round(_camera.orthographicSize) == Mathf.Round(_ortographicSize))
        {
            if (!_isZoomEffect)
            {
                _test = false;
            }
            else if (_isZoomEffect)
            {
                _zoomEffectCheck--;

            }
            _tmp = false;
            return;
        }        
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _ortographicSize, _zoomEffectSpeed);
    }
    private IEnumerator CheckEffect()
    {
        yield return new WaitForSeconds(3);
        _zoomEffectCheck = _zoomEffectCount;
        _test = true;
        Debug.Log("Test started");

    }
}
