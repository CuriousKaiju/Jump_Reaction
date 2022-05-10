using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiliAnimatorController : MonoBehaviour
{
    private Animator _liliAnimator;
    [SerializeField] private bool _itIsButtn;
    [SerializeField] private bool _itIsShakePlatform;
    [SerializeField] private GameObject _platformCollider;
    [SerializeField] private Platform _platformScript;
    [SerializeField] private Text _shotsCountText;
    [SerializeField] private int _shotsCount;

    [SerializeField] private bool _isItPlatformForRotationButton;

    private bool switcher = true;
    private bool firsJumpOnShakePlatform = true;
    private bool gunHaveShots = true;
    
    void Start()
    {
        if(_shotsCountText != null)
        {
            _shotsCountText.text = _shotsCount.ToString();
        }
        
        _liliAnimator = GetComponent<Animator>();
    }
    public void MinusShots()
    {
        _shotsCount -= 1;
        _shotsCountText.text = _shotsCount.ToString();

        if(_shotsCount <= 0 && !_isItPlatformForRotationButton)
        {
            gunHaveShots = false;
            _platformScript.ChangeGunHaveShots();
        }
    }
    public void SetGroundedAnim()
    {
        if(_itIsButtn)
        {
            if (gunHaveShots && _shotsCount > 0)
            {
                _liliAnimator = GetComponent<Animator>();
                _liliAnimator.SetBool("PlayerOnButton", switcher); //Нажимает на кнопку
                switcher = !switcher;
            }
            else
            {
                if (_isItPlatformForRotationButton)
                {
                    _liliAnimator = GetComponent<Animator>();
                    _liliAnimator.SetBool("PlayerOnButton", switcher); //Нажимает на кнопку
                    switcher = !switcher;
                }
                return;
            }
        }
        else
        {
            _liliAnimator = GetComponent<Animator>();
            _liliAnimator.SetTrigger("Grounded");
            if(_itIsShakePlatform && firsJumpOnShakePlatform)
            {
                firsJumpOnShakePlatform = false;
            }
            else if(_itIsShakePlatform && !firsJumpOnShakePlatform)
            {
                _liliAnimator = GetComponent<Animator>();
                _liliAnimator.SetTrigger("Shake");
            }
        }  
    }
    public void DisablePlatform()
    {
        _platformCollider.SetActive(false);
    }
}
