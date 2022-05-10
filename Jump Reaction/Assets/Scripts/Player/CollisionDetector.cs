using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [Header("COLLISION VARIABLES")]

    [SerializeField] private float _rayLangth;

    public  GameObject _targetPlatform = null;

    private GameObject _selecteblePlatform;
    private RaycastHit _hit;

    void Start()
    {
        
    }

    void Update()
    {
        DrawRay();
    }
    private void DrawRay()
    {
        Debug.DrawRay(transform.position, transform.forward * _rayLangth, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out _hit, _rayLangth) && _hit.collider.CompareTag("Platform collider"))
        {
            _selecteblePlatform = _hit.collider.transform.parent.gameObject;

            if (_targetPlatform && _targetPlatform != _selecteblePlatform)
            {
                _targetPlatform.GetComponent<Platform>().Deselected();
                _targetPlatform = _selecteblePlatform;
                _targetPlatform.GetComponent<Platform>().Selected();
            }
            else
            {
                _targetPlatform = _selecteblePlatform;
                _targetPlatform.GetComponent<Platform>().Selected();
            }
        }
        else
        {
            if (_targetPlatform)
            {
                _targetPlatform.GetComponent<Platform>().Deselected();
                _selecteblePlatform = null;
                _targetPlatform = null;
            }
        }
    }
}
