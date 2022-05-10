using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UiManager : MonoBehaviour
{
    [Header("UI COMPONENTS")]

    [SerializeField] private int _LevelID;
    [SerializeField] private string _ChapterString;

    [SerializeField] private Text _jumpCountText;
    [SerializeField] private Animator _panelForJumpsCountAnimator;
    public int _playerScore;
    public int _jumpsCount;

    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _winMenu;

    [SerializeField] private GameObject[] _stars;
    [SerializeField] private GameObject[] _winPopUps;

    [SerializeField] private GameObject _menuWithAdd;

    [Header("SOUND SETTINGS")]

    [SerializeField] private AudioSource _back;
    [SerializeField] private float _backVolume;

    private bool _pauseIsActive;
    private void Awake()
    {
        MainGamesEvents.ActionPlayerWin += OpenWinMenu;
        MainGamesEvents.ActionCollectCoin += AddScore;
        MainGamesEvents.ActionPlayerLose += OpenLoseMenu;
        MainGamesEvents.ActionPlayerSpentJump += MinusEnergy;
        MainGamesEvents.ActionSaveLevelStatus += Save;
        MainGamesEvents.ActionPlayerHaveNoJumps += OpenMenuWithAdd;

        //MainGamesEvents.ActionAddAdditionalJumps += CloseMenuWithAdd;
    }
    private void OnDestroy()
    {
        MainGamesEvents.ActionPlayerWin -= OpenWinMenu;
        MainGamesEvents.ActionCollectCoin -= AddScore;
        MainGamesEvents.ActionPlayerLose -= OpenLoseMenu;
        MainGamesEvents.ActionPlayerSpentJump -= MinusEnergy;
        MainGamesEvents.ActionSaveLevelStatus -= Save;
        MainGamesEvents.ActionPlayerHaveNoJumps -= OpenMenuWithAdd;

        //MainGamesEvents.ActionAddAdditionalJumps -= CloseMenuWithAdd;
    }
    private void Start()
    {
        _jumpCountText.text = _jumpsCount.ToString();
    }
    public void LoadLevel(string sceneName)
    {
        ResumeGame();
        SceneManager.LoadScene(sceneName);
    }
    private void AddScore(int score)
    {

        _playerScore += score;
        switch(_playerScore)
        {
            case 1:
                _stars[0].SetActive(true);
                break;
            case 2:
                _stars[1].SetActive(true);
                break;
            case 3:
                _stars[2].SetActive(true);
                break;
        }

    }
    public void OpenPauseMenu()
    {
        if (!_pauseIsActive)
        {
            _pauseIsActive = true;
            PauseGame();
            _pauseMenu.SetActive(true);
        }
        else
        {
            _pauseIsActive = false;
            ResumeGame();
            _pauseMenu.SetActive(false);
        }
    }
    public void OpenLoseMenu()
    {
        _loseMenu.SetActive(true);
    }
    public void OpenMenuWithAdd()
    {
        _menuWithAdd.SetActive(true);
    }
    public void CloseMenuWithAdd(int jumpsCountAfterAdd) //FORE ADD
    {
        _back.volume = _backVolume;
        _jumpsCount += jumpsCountAfterAdd;
        _jumpCountText.text = _jumpsCount.ToString();
        MainGamesEvents.OnActionAddAdditionalJumps();
        _menuWithAdd.SetActive(false);
        _panelForJumpsCountAnimator.SetTrigger("AddJumps");

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _back.volume = 0.05f;

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _back.volume = _backVolume;
    }
    public void OpenWinMenu(int score)
    {
        switch (_playerScore)
        {
            case 0:
                _winPopUps[0].SetActive(true);
                break;
            case 1:
                _winPopUps[1].SetActive(true);
                break;
            case 2:
                _winPopUps[2].SetActive(true);
                break;
            case 3:
                _winPopUps[3].SetActive(true);
                break;
        }
    }
    public void MinusEnergy()
    {
        _jumpsCount -= 1;
        _jumpCountText.text = _jumpsCount.ToString();
        if(_jumpsCount == 1)
        {
            _panelForJumpsCountAnimator.SetTrigger("Red");
        }

        if (_jumpsCount == 0)
        {
            MainGamesEvents.OnActionPlayerLastJump();
        }
    }

    private void Save()
    {
        if (Directory.Exists(Application.dataPath + "/SavesForPlayer"))
        {
            string strFromJson = File.ReadAllText(Application.dataPath + "/SavesForPlayer" + _ChapterString);
            DataLoader.LevelsList levelList = JsonUtility.FromJson<DataLoader.LevelsList>(strFromJson);
            if (_playerScore > levelList.levelsList[_LevelID].starCount)
            {
                levelList.levelsList[_LevelID].starCount = _playerScore;
            }

            levelList.levelsList[_LevelID].levelOpen = true;

            if (_LevelID + 1 < levelList.levelsList.Length)
            {
                levelList.levelsList[_LevelID + 1].levelOpen = true;
            }

            strFromJson = JsonUtility.ToJson(levelList);

            File.WriteAllText(Application.dataPath + "/SavesForPlayer" + _ChapterString, strFromJson);
        }
        else
        {
            string strFromJson = File.ReadAllText(Application.persistentDataPath + "/SavesForPlayer" + _ChapterString);
            DataLoader.LevelsList levelList = JsonUtility.FromJson<DataLoader.LevelsList>(strFromJson);
            if (_playerScore > levelList.levelsList[_LevelID].starCount)
            {
                levelList.levelsList[_LevelID].starCount = _playerScore;
            }

            levelList.levelsList[_LevelID].levelOpen = true;

            if (_LevelID + 1 < levelList.levelsList.Length)
            {
                levelList.levelsList[_LevelID + 1].levelOpen = true;
            }

            strFromJson = JsonUtility.ToJson(levelList);

            File.WriteAllText(Application.persistentDataPath + "/SavesForPlayer" + _ChapterString, strFromJson);
        }
    }
 
        
}
