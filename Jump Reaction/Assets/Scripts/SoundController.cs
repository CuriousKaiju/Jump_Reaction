using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private GameObject _toggle;

    private int _currenButtonState;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Sound") != null)
        {
            _currenButtonState = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }

        if(_currenButtonState == 1)
        {
            _toggle.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            _toggle.GetComponent<Toggle>().isOn = false;
        }
    }
    public void ToggleMusic()
    {
        if(_toggle.GetComponent<Toggle>().isOn)
        {
            _audioMixer.audioMixer.SetFloat("Volume", 0.00f);
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            _audioMixer.audioMixer.SetFloat("Volume", -80.00f);
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
}
