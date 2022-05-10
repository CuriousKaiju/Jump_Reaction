using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformForGun : MonoBehaviour
{
    [SerializeField] private Vector3 _gunPlatformRotationDirection;
    [SerializeField] private Quaternion _rotationAngle;
    [SerializeField] private float _desiredTimeForRotation;
    private Quaternion _startRotationPoint;
    private Quaternion _finishRotationPoint;
    private float _elapsedTime;

    private bool _startRotation;
    private void Update()
    {
        if(_startRotation)
        {
            RotateTheGunPlatform();
        }
    }
    private void RotateTheGunPlatform()
    {
        _elapsedTime += Time.deltaTime;
        float percentageComplete = _elapsedTime / _desiredTimeForRotation;
        transform.rotation = Quaternion.Lerp(_startRotationPoint, _finishRotationPoint, percentageComplete);
        if(percentageComplete >= 1)
        {
            _startRotation = false;
        }
    }
    public void SetRotationParams()
    {
        _elapsedTime = 0;
        _startRotationPoint = transform.rotation;
        _finishRotationPoint = transform.rotation * _rotationAngle;
        _startRotation = true;
        Debug.Log(transform.localRotation);
    }
}
