using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelsUIDuck;
    [SerializeField] private GameObject[] _levelsUIClosedDuck;

    [SerializeField] private GameObject[] _levelsUISheep;
    [SerializeField] private GameObject[] _levelsUIClosedSheep;

    [SerializeField] private GameObject[] _levelsUIPenguin;
    [SerializeField] private GameObject[] _levelsUIClosedPenguin;

    [SerializeField] private GameObject _stage;

    [SerializeField] private Text[] _scorePanels;

    [SerializeField] private Text _DATAPATH;
    [SerializeField] private MainMenuEnvirenmentFunctions _meinMenuEnvirenment;


    [SerializeField] private string[] _savesNames;
    [SerializeField] private string[] _levelName;

    private string PATH_TO_SAVES;
    private string PATH_TO_SAVES_TO_MOBILE;

    public Level myLevel = new Level();
    public LevelsList myLevelsList = new LevelsList();

    private int DuckStars;
    private int SheepStars;
    private int PenguinStars;

    private int TotalStarsCount;

    private int superTotalStarsCount;

    private void Awake()
    {
        PATH_TO_SAVES = Application.dataPath + "/SavesForPlayer";
        PATH_TO_SAVES_TO_MOBILE = Application.persistentDataPath + "/SavesForPlayer";


        Load(0);
        Load(1);
        Load(2);


        DuckStars = PlayerPrefs.GetInt(_savesNames[0]);
        SheepStars = PlayerPrefs.GetInt(_savesNames[1]);
        PenguinStars = PlayerPrefs.GetInt(_savesNames[2]);


        if(PlayerPrefs.GetInt("SuperStarsCount") >= 16)
        {
            PlayerPrefs.SetInt("SheepLevelOpen", 1);
            _meinMenuEnvirenment.GetPlayerPrefsLevelsGroupsStatus();
        }
        if (PlayerPrefs.GetInt("SuperStarsCount") >= 32)
        {
            PlayerPrefs.SetInt("PenguinLevelOpen", 1);
            _meinMenuEnvirenment.GetPlayerPrefsLevelsGroupsStatus();
        }
    }
    private void Start()
    {
       
    }



    public void Save(int chapterID)
    {

        string jsonFileName = _savesNames[chapterID];
        string strOutput = JsonUtility.ToJson(myLevelsList);

        if (Directory.Exists(PATH_TO_SAVES))
        {
            File.WriteAllText(PATH_TO_SAVES + "/" + jsonFileName + ".json", strOutput);
        }
        else
        {
            Directory.CreateDirectory(PATH_TO_SAVES_TO_MOBILE);
            File.WriteAllText(PATH_TO_SAVES_TO_MOBILE + "/" + jsonFileName + ".json", strOutput);
        }
    }


    public void Load(int chapterID)
    {

        string jsonFileName = _savesNames[chapterID];

        if (Directory.Exists(PATH_TO_SAVES))
        {
            string strInput = File.ReadAllText(PATH_TO_SAVES + "/" + jsonFileName + ".json");
            myLevelsList = JsonUtility.FromJson<LevelsList>(strInput);
        }
        else if(Directory.Exists(Application.persistentDataPath))
        {
            if(File.Exists(PATH_TO_SAVES_TO_MOBILE + "/" + jsonFileName + ".json"))
            {
                string strInput = File.ReadAllText(PATH_TO_SAVES_TO_MOBILE + "/" + jsonFileName + ".json");
                myLevelsList = JsonUtility.FromJson<LevelsList>(strInput);
            }
            else
            {
                Directory.CreateDirectory(PATH_TO_SAVES_TO_MOBILE);
                File.WriteAllText(PATH_TO_SAVES_TO_MOBILE + "/" + jsonFileName + ".json", JsonUtility.ToJson(myLevelsList));
            }
        }


        switch (jsonFileName)
        {
            case "Duck":
                SetLevelsStatusDuck(jsonFileName);
                
                break;

            case "Sheep":
                SetLevelsStatusSheep(jsonFileName);
                
                break;

            case "Penguin":
                
                SetLevelsStatusPenguin(jsonFileName);
                break;
        }
    }

    public void SetLevelsStatusDuck(string nameOFLevel)
    {
        TotalStarsCount = 0;

        for (int i = 0; i < _levelsUIDuck.Length; i++)
        {
            if (myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUIDuck[i].SetActive(true);
                _levelsUIDuck[i].GetComponent<LevelObject>().SetParamsOfLevel(myLevelsList.levelsList[i].starCount, myLevelsList.levelsList[i].levelID, nameOFLevel + " " + myLevelsList.levelsList[i].levelID);
                TotalStarsCount += myLevelsList.levelsList[i].starCount;

                _levelsUIClosedDuck[i].SetActive(false);
            }
            else if (!myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUIClosedDuck[i].SetActive(true);
                _levelsUIDuck[i].SetActive(false);
            }
        }

        DuckStars = TotalStarsCount;
        PlayerPrefs.SetInt(_savesNames[0], DuckStars);
        superTotalStarsCount = DuckStars + SheepStars + PenguinStars;
        PlayerPrefs.SetInt("SuperStarsCount", superTotalStarsCount);

        foreach (Text text in _scorePanels)
        {
            text.text = superTotalStarsCount.ToString();
        }
    }
    public void SetLevelsStatusSheep(string nameOFLevel)
    {
        TotalStarsCount = 0;

        for (int i = 0; i < _levelsUISheep.Length; i++)
        {
            if (myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUISheep[i].SetActive(true);
                _levelsUISheep[i].GetComponent<LevelObject>().SetParamsOfLevel(myLevelsList.levelsList[i].starCount, myLevelsList.levelsList[i].levelID, nameOFLevel + " " + myLevelsList.levelsList[i].levelID);
                TotalStarsCount += myLevelsList.levelsList[i].starCount;
                //
                _levelsUIClosedSheep[i].SetActive(false);
            }
            else if (!myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUIClosedSheep[i].SetActive(true);
                _levelsUISheep[i].SetActive(false);
            }
        }

        SheepStars = TotalStarsCount;
        PlayerPrefs.SetInt(_savesNames[1], SheepStars);
        superTotalStarsCount = DuckStars + SheepStars + PenguinStars;
        PlayerPrefs.SetInt("SuperStarsCount", superTotalStarsCount);

        foreach (Text text in _scorePanels)
        {
            text.text = (DuckStars + SheepStars + PenguinStars).ToString();
        }
    }
    public void SetLevelsStatusPenguin(string nameOFLevel)
    {
        TotalStarsCount = 0;

        for (int i = 0; i < _levelsUIPenguin.Length; i++)
        {
            if (myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUIPenguin[i].SetActive(true);
                _levelsUIPenguin[i].GetComponent<LevelObject>().SetParamsOfLevel(myLevelsList.levelsList[i].starCount, myLevelsList.levelsList[i].levelID, nameOFLevel + " " + myLevelsList.levelsList[i].levelID);
                TotalStarsCount += myLevelsList.levelsList[i].starCount;
                //
                _levelsUIClosedPenguin[i].SetActive(false);
            }
            else if (!myLevelsList.levelsList[i].levelOpen)
            {
                _levelsUIClosedPenguin[i].SetActive(true);
                _levelsUIPenguin[i].SetActive(false);
            }
        }

        PenguinStars = TotalStarsCount;
        PlayerPrefs.SetInt(_savesNames[2], PenguinStars);
        superTotalStarsCount = DuckStars + SheepStars + PenguinStars;
        PlayerPrefs.SetInt("SuperStarsCount", superTotalStarsCount);
        

        foreach (Text text in _scorePanels)
        {
            text.text = (DuckStars + SheepStars + PenguinStars).ToString();
        }
    }

    [System.Serializable]
    public class LevelsList
    {
        public Level[] levelsList;
    }
    [System.Serializable]
    public class Level
    {
        public int levelID = 0;
        public int starCount = 0;
        public bool levelOpen = false;
    }
}
