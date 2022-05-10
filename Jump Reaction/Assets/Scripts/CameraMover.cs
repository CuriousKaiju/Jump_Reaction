using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private float _distanceBetweenPlayerAndCamara;
    [SerializeField] private float _desiredCameraMovementTime;

    private bool _move = false;
    private float _elapsedTime;
    private Vector3 _playerCurrentPosition;
    private Vector3 _enablePos;

    private float _distanseBetweenPlayerAndCamera;


    private void Start()
    {
        _playerCurrentPosition = _player.GetComponent<Transform>().TransformPoint(0, 0, 0);
        _distanseBetweenPlayerAndCamera = Vector3.Distance(new Vector3(0, 0, transform.position.z), new Vector3(0, 0, _player.transform.position.z));

    }

    void Update()
    {
        if(_move)
        {
            CameraPositionChanger();
        }
    }
    private void CameraPositionChanger()
    {
        _elapsedTime += Time.deltaTime;
        float _perocentageComplete = _elapsedTime / _desiredCameraMovementTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, Vector3.Lerp(transform.position, _playerCurrentPosition, _curve.Evaluate(_perocentageComplete)).z);
        if (_perocentageComplete >= 1)
        {
            _elapsedTime = 0;
            _move = false;
        }
    }
    public void StarMove(Vector3 target)
    {
        _playerCurrentPosition = new Vector3(target.x, target.y, target.z - _distanseBetweenPlayerAndCamera);
        _move = true;
    }
}
