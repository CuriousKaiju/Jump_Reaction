using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsUI : MonoBehaviour
{

    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject[] _closedLevels;
    [SerializeField] private int[] _levelsStatus;
    [SerializeField] private int[] _strsOnLevel;
    void Start()
    {
        SortStageMenu();
    }
    void Update()
    {

    }
    private void SortStageMenu()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            if (_levelsStatus[i] == 1)
            {
                _levels[i].SetActive(true);
            }
            else
            {
                _closedLevels[i].SetActive(true);
            }
        }
    }
    private void SetLevelsStatus()
    {
        for (int i = 0; i < _levelsStatus.Length; i++)
        {
            
        }
    }
    private void GetlevelVariables()
    {
        for (int i = 0; i < _levelsStatus.Length; i++)
        {
            if (_levelsStatus[i] == 1)
            {
                _levels[i].SetActive(true);
            }
            else
            {
                _closedLevels[i].SetActive(true);
            }
        }
    }
}
