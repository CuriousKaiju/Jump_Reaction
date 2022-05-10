using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    [SerializeField] private Text _levelID;
    [SerializeField] private GameObject _lpadLevel;
    private int _levelIDForLoad;
    private int _starsCount;
    private string _levelName;

    private void Start()
    {
        SetLevelParams();
    }
    private void SetLevelParams()
    {
        switch (_starsCount)
        {
            case 0:
                _stars[0].SetActive(false);
                _stars[1].SetActive(false);
                _stars[2].SetActive(false);
                break;
            case 1:
                _stars[0].SetActive(true);
                _stars[1].SetActive(false);
                _stars[2].SetActive(false);
                break;
            case 2:
                _stars[0].SetActive(true);
                _stars[1].SetActive(true);
                _stars[2].SetActive(false);
                break;
            case 3:
                _stars[0].SetActive(true);
                _stars[1].SetActive(true);
                _stars[2].SetActive(true);
                break;
        }
    }
    public void SetParamsOfLevel(int starsCount, int levelID, string levelName)
    {
        _starsCount = starsCount;
        _levelID.text = levelID.ToString();
        _levelName = levelName;
        SetLevelParams();
    }
    public void LoadLevel()
    {
        _lpadLevel.SetActive(true);
        SceneManager.LoadScene(_levelName);
    }

}
