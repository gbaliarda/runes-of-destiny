using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class GameLevelManager : MonoBehaviour
{
    #region SINGLETON
    static public GameLevelManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    public void LoadMainMenu() => SceneManager.LoadScene((int)Levels.MainMenu);
    public void LoadLoadScreen() => SceneManager.LoadScene((int)Levels.LoadScreen);
    public void LoadLevel1() => SceneManager.LoadScene((int)Levels.Level1);
    public void Exit() => Application.Quit();
}
