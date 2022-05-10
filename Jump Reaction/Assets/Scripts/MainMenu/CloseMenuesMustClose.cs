using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenuesMustClose : MonoBehaviour
{
    [SerializeField] private MainMenuEnvirenmentFunctions _scriptMainMenu;
    [SerializeField] private Button _playButton;
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    public void OpenLevelsForever(string levelsGroupName)
    {
        gameObject.GetComponent<Animator>().SetTrigger("CloseMenu");
        PlayerPrefs.SetInt(levelsGroupName, 1);
        _scriptMainMenu.GetPlayerPrefsLevelsGroupsStatus();
        _playButton.interactable = true;
    }

}
