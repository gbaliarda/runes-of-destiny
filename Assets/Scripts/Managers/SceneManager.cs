using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private string _logInSceneName;

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_logInSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
