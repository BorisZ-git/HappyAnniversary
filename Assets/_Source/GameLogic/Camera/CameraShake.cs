using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake
{
    private Transform _cameraTransform;
    private float x, y;
    public CameraShake(Transform camera)
    {
        _cameraTransform = camera;
    }

    public void Shake()
    {
        x = Random.Range(-0.3f, 0.3f);
        y = Random.Range(-0.3f, 0.3f);
        _cameraTransform.position = new Vector3(_cameraTransform.position.x + x, _cameraTransform.position.y + y, _cameraTransform.position.z);
    }
}
