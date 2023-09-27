using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    private Resolution[] _resolutions;
    private int _resolution;
    private int _tempResolution;
    private bool _fullScreen;
    private float _volume;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Toggle _fullScreenToggle;

    public Resolution Resolution => _resolutions[_resolution];
    public bool FullScreen => _fullScreen;
    public float Volume => _volume;

    private void LoadVariables()
    {
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null )
        {
            _volume = settingsData.volume;
            Resolution res = new Resolution();
            res.width = settingsData.resolutionWidth;
            res.height = settingsData.resolutionHeight;
            for (int i = 0; i < _resolutions.Length; i++)
                if (_resolutions[i].width == res.width && _resolutions[i].height == res.height)
                {
                    _resolution = i;
                    break;
                }
            _fullScreen = settingsData.fullScreen;
        } else
        {
            _volume = 1;
            for (int i = 0; i < _resolutions.Length; i++)
                if (_resolutions[i].width == 1920 && _resolutions[i].height == 1080)
                {
                    _resolution = i;
                    break;
                }
            _fullScreen = true;
            SaveSystem.SaveSettings(this);
        }
    }
    void Start()
    {
        _resolutions = Screen.resolutions;
        LoadVariables();

        _resolutionDropdown.ClearOptions();
        List<string> resolutionString = new List<string>();
        foreach (Resolution res in  _resolutions) resolutionString.Add($"{res.width}x{res.height}");
        _resolutionDropdown.AddOptions(resolutionString);
        _resolutionDropdown.value = _resolution;
        _resolutionDropdown.RefreshShownValue();

        gameObject.SetActive(false);
        _slider.value = _volume;
        _audioSource.volume = _volume;

        _fullScreenToggle.isOn = _fullScreen;
    }

    public void OpenSettings()
    {
        gameObject.SetActive(true);
    }
    public void CloseSettings()
    {
        gameObject.SetActive(false);
        _audioSource.volume = _volume;
        _slider.value = _volume;
        Screen.fullScreen = _fullScreen;
        _fullScreenToggle.isOn = _fullScreen;
        _tempResolution = _resolution;
        _resolutionDropdown.value = _resolution;
        _resolutionDropdown.RefreshShownValue();
    }

    public void ChangeResolution(int resolution)
    {
        _tempResolution = resolution;
        _resolutionDropdown.value = resolution;
        _resolutionDropdown.RefreshShownValue();
        Screen.SetResolution(_resolutions[resolution].width, _resolutions[resolution].height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetVolumne(float volume)
    {
        _audioSource.volume = volume;
    }

    public void Save()
    {
        gameObject.SetActive(false);
        _volume = _audioSource.volume;
        _fullScreen = Screen.fullScreen;
        _resolution = _tempResolution;
        SaveSystem.SaveSettings(this);
    }
}
