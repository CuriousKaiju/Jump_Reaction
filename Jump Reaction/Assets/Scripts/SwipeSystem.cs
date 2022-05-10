using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSystem : MonoBehaviour
{

    [SerializeField] private float _swipeRange;
    [SerializeField] private float _tapRange;

    [SerializeField] private MainMenuEnvirenmentFunctions _scriptOfMainMenu;

    private Vector2 _startTouchPos;
    private Vector2 _currentTouchPos;
    private Vector2 _endTouchPos;
    private bool _stopTouch = false;
 
    void Update()
    {
        Swipe();
    }
    private void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            _currentTouchPos = Input.GetTouch(0).position;
            Vector2 distance = _currentTouchPos - _startTouchPos;

            if (!_stopTouch)
            {
                if (distance.x < -_swipeRange)
                {
                    _stopTouch = true;
                    _scriptOfMainMenu.TurnRight();
                }
                else if (distance.x > _swipeRange)
                {
                    _stopTouch = true;
                    _scriptOfMainMenu.TurnLeft();
                    
                }
                else if (distance.y < -_swipeRange)
                {
                    _stopTouch = true;
                    Debug.Log(3);
                    //down
                }
                else if (distance.y > _swipeRange)
                {
                    _stopTouch = true;
                    Debug.Log(4);
                    //up
                }

            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            _stopTouch = false;

            _endTouchPos = Input.GetTouch(0).position;

            Vector2 distance = _endTouchPos - _startTouchPos;

            if(Mathf.Abs(distance.x) < _tapRange && Mathf.Abs(distance.y) < _tapRange)
            {

            }

        }



    }
}
