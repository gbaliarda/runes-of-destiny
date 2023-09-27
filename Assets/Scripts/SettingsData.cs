using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float volume;
    public bool fullScreen;
    public int resolutionWidth;
    public int resolutionHeight;

    public SettingsData(SettingsManager settingsManager)
    {
        volume = settingsManager.Volume;
        fullScreen = settingsManager.FullScreen;
        Debug.Log(fullScreen);
        resolutionWidth = settingsManager.Resolution.width;
        resolutionHeight = settingsManager.Resolution.height;
    }
}
