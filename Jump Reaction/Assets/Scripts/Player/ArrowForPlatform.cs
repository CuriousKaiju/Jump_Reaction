using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowForPlatform : MonoBehaviour
{
    private Animator _arrowAnimator;
    void Start()
    {
        _arrowAnimator = GetComponent<Animator>();
    }
    public void SetArrowAnimationAppear()
    {
        _arrowAnimator.SetBool("Grounded", true);
    } 
    public void SetArrowAnimationDesappear()
    {
        _arrowAnimator.SetBool("Grounded", false);
    }
}
