using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("PLATFORM COMPONENTS")]

    [SerializeField] private GameObject _waveParticles;
    [SerializeField] private GameObject _playerGroundedPointOfPlatform;
    [SerializeField] private GameObject _visualOfPlatform;
    private LiliAnimatorController _liliAnimatorControllerScript;

    [SerializeField] private GameObject _colliderOfPlatform;

    [Header("ROTATION VARIABLES")]

    [SerializeField] private Color _standartColor;
    [SerializeField] private Color _selectionColor;
    private Material startMaterial;
    public bool _itIsFinishPlatform;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private Vector3 _desiredRotationDirection;
    private bool _playerAbovePlatform;

    [Header("SOUNDS VARIABLES")]
    [SerializeField] private AudioSource _liliSound;
    [SerializeField] private AudioSource _starCollectedSound;
    [SerializeField] private AudioSource _gunHaveNoShots;
    private bool _gunHaveShotsBool = true;
    [SerializeField] private bool _isThisPlatformLikeRotationButton;

    [SerializeField] private bool _starOnThisPlatform;

    
    public void ChangeGunHaveShots()
    {
        if(_isThisPlatformLikeRotationButton)
        {
            //Debug.Log("You Jumped on rotation Button");
        }
        else
        {
            _gunHaveShotsBool = false;
        }
    }
    void Start()
    {
        _liliAnimatorControllerScript = _visualOfPlatform.GetComponent<LiliAnimatorController>();
        startMaterial = _visualOfPlatform.GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        if(_playerAbovePlatform)
        {
            PlatformRotation();
        }
    }
    public void Selected()
    {
        startMaterial.color = _selectionColor;
    }
    public void Deselected()
    {
        _visualOfPlatform.GetComponent<MeshRenderer>().material.color = _standartColor;
    }
    public void PlayerGrounded(Vector3 rotationDirection)
    {

        if (_starOnThisPlatform)
        {
            _starCollectedSound.Play();
            _starOnThisPlatform = false;
        }
        else if (!_gunHaveShotsBool)
        {
            _gunHaveNoShots.Play();
        }
        else
        {
            _liliSound.Play();
        }

        Instantiate(_waveParticles, _visualOfPlatform.transform.position, Quaternion.identity);
        _liliAnimatorControllerScript.SetGroundedAnim();

        if (_desiredRotationDirection == new Vector3(0, 0, 0))
        {
            _rotationDirection = rotationDirection;
        }
        else
        {
            _rotationDirection = _desiredRotationDirection;
        }

        _playerAbovePlatform = true;
    }
    public void PlayerJumped()
    {
        _liliAnimatorControllerScript.SetGroundedAnim();
        _playerAbovePlatform = false;
    }
    private void PlatformRotation()
    {
        transform.Rotate(_rotationDirection, _rotationSpeed * Time.deltaTime);
    }
    public Vector3 GetCurrentPlatformTransform()
    {
        return _playerGroundedPointOfPlatform.transform.TransformPoint(0, 0, 0);
    }
    public void ChangeRotationDiretcion(Vector3 rotationDirection)
    {
        if(_desiredRotationDirection == new Vector3(0, 0, 0))
        {
            _rotationDirection = rotationDirection;
        }
        else
        {
            _rotationDirection = _desiredRotationDirection;
        }
    }

}
