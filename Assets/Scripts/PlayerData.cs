using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int volume;
    public bool fullScreen;
    public int resolutionWidth;
    public int resolutionHeight;

    public PlayerData(int volume, bool fullScreen, int resolutionWidth, int resolutionHeight)
    {
        this.volume = volume;
        this.fullScreen = fullScreen;
        this.resolutionWidth = resolutionWidth;
        this.resolutionHeight = resolutionHeight;
    }
}
