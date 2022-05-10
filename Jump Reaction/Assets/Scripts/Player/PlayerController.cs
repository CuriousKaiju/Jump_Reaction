using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("PLAYER COMPONENTS")]

    [SerializeField] private GameObject _camera;
    private CameraMover _cameraMoverScript;


    [SerializeField] private GameObject _waveParticle;
    [SerializeField] private GameObject _visualOfPlayer;
    [SerializeField] private GameObject _collisionDetector;
    [SerializeField] private GameObject _loseTargetPoint;
    [SerializeField] private GameObject _currentPlatform;
    private GameObject _targetPlatformBeforeJump;

    private Vector3 _targetPlatformTransform;
    private Vector3 _currentPlatformTransform;

    [Header("JUMP VARIABLES")]

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _desiredTimeForJump;
    [SerializeField] private GameObject _arrowOfPlayerDirectin;
    private ArrowForPlatform _arrowAnimationControllerScript;

    [SerializeField] private bool _playerWasEated = true;

    private bool _haveNoJumpsWasCalled = false;
    private bool _haveNoJumps = false;
    private bool _loseGrounded = false;
    private bool _itIsLoseJump = false;
    private bool _buttonForJump = false;
    private bool _playerCanJump = true;
    private bool _isItFirstJump = true;
    private float _elapsedTime;

    [Header("PLATFORM INFLUENCE")]

    private bool _rightRotation = true;
    private Vector3 _rotationDirection;

    [Header("UI COMPONENTS")]

    [SerializeField] private GameObject _loseMenu;

    [Header("SOUNDS VARIABLES")]
    [SerializeField] private AudioSource _loseInWaterSound;
    [SerializeField] private AudioSource _flowerBite;
    [SerializeField] private AudioSource _back;
    [SerializeField] private AudioSource _finishSound;
    private bool _stunSoundWasPlayed = false;

    [SerializeField] private AudioSource _playerStun;
    private bool _stunStartPlayed = false;
    [SerializeField] private float _desiredTimeForStunFade;
    private float _elapsedTimeForStun;

    private void Awake()
    {
        MainGamesEvents.ActionPlayerLastJump += ChangeLastJumpVariavle;
        MainGamesEvents.ActionAddAdditionalJumps += TheJumpsWasAdded;
    }
    private void OnDestroy()
    {
        MainGamesEvents.ActionPlayerLastJump -= ChangeLastJumpVariavle;
        MainGamesEvents.ActionAddAdditionalJumps -= TheJumpsWasAdded;
    }

    public void PlayStunSound(bool wasEated)
    {
        if (!wasEated)
        {
            _stunStartPlayed = true;
            _playerStun.volume = 1;
            _playerStun.Play();
        }

        _back.volume = 0;

    }
    void Start()
    {
        _cameraMoverScript = _camera.GetComponent<CameraMover>();
        _arrowAnimationControllerScript = _arrowOfPlayerDirectin.GetComponent<ArrowForPlatform>();
    }
    void Update()
    {

        if(_buttonForJump && _playerCanJump)
        {
            Tap();
        }

        if(!_playerCanJump && !_loseGrounded)
        {
            Jump();
        }
        
        if(_stunStartPlayed)
        {
            _elapsedTimeForStun += Time.deltaTime;
            float percentageComplete = _elapsedTimeForStun / _desiredTimeForStunFade;
            _playerStun.volume = Mathf.Lerp(1, 0, percentageComplete);
            if(percentageComplete >= 1)
            {
                _stunStartPlayed = false;
                _elapsedTimeForStun = 0;
            }

        }

    }
    private void Tap()
    {


        MainGamesEvents.OnActionPlayerSpentJump();
        if (_collisionDetector.GetComponent<CollisionDetector>()._targetPlatform)
        {
            _targetPlatformTransform = _collisionDetector.GetComponent<CollisionDetector>()._targetPlatform.GetComponent<Platform>().GetCurrentPlatformTransform();
            _targetPlatformBeforeJump = _collisionDetector.GetComponent<CollisionDetector>()._targetPlatform;
        }
        else
        {
            _targetPlatformTransform = _loseTargetPoint.transform.TransformPoint(0, 0, 0);
            _itIsLoseJump = true;
        }



        _arrowAnimationControllerScript.SetArrowAnimationDesappear();

        if (!_isItFirstJump)
        {
            _cameraMoverScript.StarMove(_targetPlatformTransform);
        }

        transform.parent = null;
        _currentPlatform.GetComponent<Platform>().PlayerJumped();
        _currentPlatformTransform = _currentPlatform.GetComponent<Platform>().GetCurrentPlatformTransform();
        _visualOfPlayer.GetComponent<PlayerAnimation>().ActivateJumpAnimation();
        _playerCanJump = false;

        _collisionDetector.SetActive(false);
    }
    private void Jump()
    {
        _elapsedTime += Time.deltaTime;
        float percontageComplete = _elapsedTime / _desiredTimeForJump;
        transform.position = Vector3.Lerp(_currentPlatformTransform, _targetPlatformTransform, _curve.Evaluate(percontageComplete));
        if(percontageComplete >= 1)
        {
            if(!_itIsLoseJump)
            {
                Grounded();
            }
            else
            {
                LoseGrounded();
            }
        }
    }
    private void Grounded()
    {

        if (_isItFirstJump)
        {
            _isItFirstJump = false;
            _cameraMoverScript.enabled = true;

        }

        _currentPlatform = _targetPlatformBeforeJump;
        if (_rightRotation)
        {
            _rotationDirection = new Vector3(0, 1, 0);
            _rightRotation = !_rightRotation;
        }
        else
        {
            _rotationDirection = new Vector3(0, -1, 0);
            _rightRotation = !_rightRotation;
        }


        if (!_playerWasEated && !_targetPlatformBeforeJump.GetComponent<Platform>()._itIsFinishPlatform && !_haveNoJumps)
        {
            _arrowAnimationControllerScript.SetArrowAnimationAppear();
            _buttonForJump = false;
            _currentPlatform.GetComponent<Platform>().PlayerGrounded(_rotationDirection);
            transform.parent = _currentPlatform.transform;
            _elapsedTime = 0;
            _playerCanJump = true;
        }
        else if (_targetPlatformBeforeJump.GetComponent<Platform>()._itIsFinishPlatform)
        {
            _rotationDirection = new Vector3(0, 0, 0);
            FinishGrounded();
        }
        else if (_haveNoJumps && !_haveNoJumpsWasCalled)
        {
            PlayStunSound(_playerWasEated);
            _haveNoJumpsWasCalled = true;
            _rotationDirection = new Vector3(0, 0, 0);
            _visualOfPlayer.GetComponent<PlayerAnimation>().ActivateLoseAnimationAfterLastJump(); //
        }

        _collisionDetector.SetActive(true);


    }
    private void TheJumpsWasAdded()
    {
        _haveNoJumpsWasCalled = false;
        _rotationDirection = new Vector3(0, 1, 0);
        _visualOfPlayer.GetComponent<PlayerAnimation>().AnimationAfterAddiotionalJumps();

        _playerCanJump = true;
        _loseGrounded = false;
        _haveNoJumps = false;
        Grounded();
    }

    private void ChangeLastJumpVariavle()
    {
        _haveNoJumps = true;
    }
    private void FinishGrounded()
    {
        MainGamesEvents.OnActionSaveLevelStatus();
        _back.volume = 0;
        _visualOfPlayer.GetComponent<PlayerAnimation>().ActivateWinJumpAnimation();

        if(!_stunSoundWasPlayed)
        {
            _finishSound.Play();
            _stunSoundWasPlayed = true;
        }
    }
    private void LoseGrounded()
    {
        _loseGrounded = true;
        Instantiate(_waveParticle, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        _loseInWaterSound.Play();
        _back.volume = 0;

        MainGamesEvents.OnActionPlayerLose();
    }    
    public void ButtonPress()
    {
       //_camera.GetComponent<CameraMoverBeforeGamesStart>().enabled = false;
        _buttonForJump = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Flower"))
        {
            _back.volume = 0;
            _flowerBite.Play();
            _playerWasEated = true;
            _visualOfPlayer.GetComponent<PlayerAnimation>().SkaleAnimation();
            other.GetComponent<Flower>().PlayAttackAnim();
        }
    }
}
