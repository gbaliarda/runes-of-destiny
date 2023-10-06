using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enums;

public class LoadScreenManager : MonoBehaviour
{
    #region SINGLETON
    static public LoadScreenManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    [SerializeField] private Slider _slider;

    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }
    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)Levels.Level1);

        float progress = 0f;

        while (!operation.isDone) { 
            progress = operation.progress;

            _slider.value = progress;

            yield return null;
        }
    }
}
