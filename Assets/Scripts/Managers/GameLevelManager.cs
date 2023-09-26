using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void LoadMainMenu() => SceneManager.instance.LoadScene((int)Levels.MainMenu);
    public void LoadLevel1() => SceneManager.instance.LoadScene((int)Levels.Level1);
    public void Exit() => SceneManager.instance.ExitGame();
}
