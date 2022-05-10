using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimator;
    private int _score;
    [SerializeField] private GameObject confity;
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
    }
    public void ActivateJumpAnimation()
    {
        _playerAnimator.SetTrigger("jump");
    }
    public void SkaleAnimation()
    {
        _playerAnimator.SetTrigger("flower");
    }
    public void ActivateWinJumpAnimation()
    {
        _playerAnimator.SetTrigger("win jump");
    }
    public void ActivateLoseAnimationAfterLastJump()
    {
        _playerAnimator.SetTrigger("stun");
    }
    public void AnimationAfterAddiotionalJumps()
    {
        _playerAnimator.SetTrigger("Add");
    }
    public void CallWinAction()
    {
        MainGamesEvents.OnActionPlayerWin(_score);
    }
    public void CallLoseAction()
    {
        MainGamesEvents.OnActionPlayerLose();
    }
    public void OpenAddPopUp()
    {
        MainGamesEvents.OnActionPlayerHaveNoJumps();
    }
    public void CallLoseActionFirst()
    {
        Instantiate(confity, transform.position, Quaternion.identity);
    }
}
