using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuEnvirenmentFunctions : MonoBehaviour
{
    [Header("ENVIRENMENT VARIABLES")]

    [SerializeField] private Button _PlayButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private GameObject _leftButton;

    [SerializeField] private Vector3[] _posOfEnvirenment;
    [SerializeField] private GameObject _envirenmentObj;

    [SerializeField] private GameObject[] _levelMenu;

    [SerializeField] private float _desireTimeForRotation;

    [SerializeField] private DataLoader _dataLoader;

    [SerializeField] private int _desiredStarsForSheepLevel;
    [SerializeField] private int _desiredStarsForPenguinLevel;

    [SerializeField] private GameObject SheepCloseMenu;
    [SerializeField] private GameObject PenguinCloseMenu;

    [Header("SOUND VARIABLES")]

    [SerializeField] private AudioSource _buttonPressSound;

    private float _elapsedTime;
    private int _nextLocationID;

    private int _sheepLevelAlredyOpen;
    private int _penguinLevelAlredyOpen;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SheepLevelOpen") && !PlayerPrefs.HasKey("PenguinLevelOpen"))
        {
            PlayerPrefs.SetInt("SheepLevelOpen", 0);
            PlayerPrefs.SetInt("PenguinLevelOpen", 0);  
        }
        GetPlayerPrefsLevelsGroupsStatus();

    }
    public void GetPlayerPrefsLevelsGroupsStatus()
    {
        _sheepLevelAlredyOpen = PlayerPrefs.GetInt("SheepLevelOpen");
        _penguinLevelAlredyOpen = PlayerPrefs.GetInt("PenguinLevelOpen");
    }
    void Update()
    {
        Rotation(_nextLocationID);
    }
    private void Rotation(int locationID)
    {
        _elapsedTime += Time.deltaTime;
        float perocentageComplete = _elapsedTime / _desireTimeForRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_posOfEnvirenment[locationID]), perocentageComplete);
        if(perocentageComplete >= 0)
        {
            _elapsedTime = 0;
        }
    }
    
    public void TurnRight()
    {
        
        if (_posOfEnvirenment.Length - 1 > _nextLocationID)
        {

            _buttonPressSound.Play();
            _leftButton.SetActive(true);

            _nextLocationID += 1;
            switch(_nextLocationID)
            {
                case 0:
                    _PlayButton.interactable = true;
                    SheepCloseMenu.SetActive(false);
                    break;
                case 1:
                    if(_sheepLevelAlredyOpen == 0)
                    {
                        SheepCloseMenu.SetActive(true);
                        SheepCloseMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
                        _PlayButton.interactable = false;
                    }
                    break;
                case 2:
                    if(_penguinLevelAlredyOpen == 0)
                    {
                        SheepCloseMenu.GetComponent<Animator>().SetTrigger("CloseMenu");
                        SheepCloseMenu.SetActive(false);
                        PenguinCloseMenu.SetActive(true);
                        PenguinCloseMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
                        _PlayButton.interactable = false;
                        
                    }
                    else
                    {
                        _PlayButton.interactable = true;
                        SheepCloseMenu.SetActive(false);
                    }
                    break;
            }


            if (_posOfEnvirenment.Length -1  == _nextLocationID)
            {
                _rightButton.SetActive(false);
            }
        }
    }
    public void TurnLeft()
    {
        
        if (_nextLocationID > 0)
        {
            _buttonPressSound.Play();
            _rightButton.SetActive(true);

            _nextLocationID -= 1;


            switch (_nextLocationID)
            {
                case 0:
                    SheepCloseMenu.SetActive(false);
                    SheepCloseMenu.GetComponent<Animator>().SetTrigger("CloseMenu");
                    _PlayButton.interactable = true;
                    break;

                case 1:
                    if (_sheepLevelAlredyOpen == 0)
                    {
                        SheepCloseMenu.SetActive(true);
                        SheepCloseMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
                        PenguinCloseMenu.GetComponent<Animator>().SetTrigger("CloseMenu");
                        _PlayButton.interactable = false;
                    }
                    else
                    {
                        PenguinCloseMenu.GetComponent<Animator>().SetTrigger("CloseMenu");
                        PenguinCloseMenu.SetActive(false);
                        _PlayButton.interactable = true;
                    }
                    break;

                case 2:
                    if(_penguinLevelAlredyOpen == 0)
                    {
                        SheepCloseMenu.GetComponent<Animator>().SetTrigger("CloseMenu");
                        PenguinCloseMenu.SetActive(true);
                        PenguinCloseMenu.GetComponent<Animator>().SetTrigger("OpenMenu");
                        _PlayButton.interactable = false;
                    }
                    break;
            }


            if (_nextLocationID == 0)
            {
                _leftButton.SetActive(false);
            }
        }
    }
    public void OpenLevelMenu()
    {
        _dataLoader.Load(_nextLocationID);
        _levelMenu[_nextLocationID].SetActive(true);
    }
    public void CloseLevelMenu()
    {
        _levelMenu[_nextLocationID].SetActive(false);
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
    public void CleanPlayerPrefsForAdditionalsLevel()
    {
        PlayerPrefs.DeleteKey("SheepLevelOpen");
        PlayerPrefs.DeleteKey("PenguinLevelOpen");
    }
}
