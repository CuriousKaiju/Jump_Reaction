using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLocationMover : MonoBehaviour
{
    [Header("START LOCATION VARIABLES")]
    
    [SerializeField] private Transform _downPoint;
    [SerializeField] private float _desiredTimeForMove;

    private Vector3 _startPos;
    private float _elapsedTime;

    private bool _startMove;
    void Start()
    {
        _startPos = transform.position;
    }
    void Update()
    {
        if(_startMove)
        {
            Move();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _downPoint.parent = null;
            _startMove = true;
        }
    }
    private void Move()
    {
        _elapsedTime += Time.deltaTime;
        float perocentageCompleate = _elapsedTime / _desiredTimeForMove;
        transform.position = Vector3.Lerp(_startPos, _downPoint.position, perocentageCompleate);
    }
}
