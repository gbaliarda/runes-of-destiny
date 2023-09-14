using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private SceneManager instance;
    [SerializeField] private string _logInSceneName;

    void Awake()
    {
        if (instance != null) Destroy(this);
        instance = this;
    }

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_logInSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
