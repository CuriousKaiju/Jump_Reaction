using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoverBeforeGamesStart : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _finishPos;
    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private AnimationCurve _curveForFastMove;

    [SerializeField] private float _desiredTimeForCameraMove;
    [SerializeField] private float _desiredTimeForFastCameraMove;

    [SerializeField] private GameObject _fastMoveButton;
    [SerializeField] private GameObject _PlayerController;
    private float _elapsedTime;
    private float _elapsedTimeForFastMove;
    private bool fastMove = false;

    private bool _cameraMustMove = true;
    void Start()
    {
        
    }
    void Update()
    {
        if(_cameraMustMove && !fastMove)
        {
            CamerMove();
        }
        else if(_cameraMustMove && fastMove)
        {
            FastMiveCamera();
        }
        
    }
    private void CamerMove()
    {
        _elapsedTime += Time.deltaTime;
        float precentageComplete = _elapsedTime / _desiredTimeForCameraMove;
        transform.position = Vector3.Lerp(_startPos.position, _finishPos.position, _curve.Evaluate(precentageComplete));
        transform.rotation = Quaternion.Lerp(_startPos.rotation, _finishPos.rotation, _curve.Evaluate(precentageComplete));
        if(precentageComplete >= 1)
        {
            //GetComponent<CameraMover>().enabled = true;
            _fastMoveButton.SetActive(false);
            _cameraMustMove = false;
        }

    }
    private void FastMiveCamera()
    {
        _elapsedTimeForFastMove += Time.deltaTime;
        float precentageComplete = _elapsedTimeForFastMove / _desiredTimeForFastCameraMove;
        transform.position = Vector3.Lerp(transform.position, _finishPos.position, _curve.Evaluate(precentageComplete));
        transform.rotation = Quaternion.Lerp(transform.rotation, _finishPos.rotation, _curve.Evaluate(precentageComplete));
        if (precentageComplete >= 1)
        {
            //GetComponent<CameraMover>().enabled = true;
            _cameraMustMove = false;
        }
    }
    public void StartFastMoveMode()
    {
        _PlayerController.GetComponent<PlayerController>().ButtonPress();
        _fastMoveButton.SetActive(false);
        fastMove = true;
    }
}
