using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    #region SINGLETON
    static public MenuManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject _menu;
    [SerializeField] private string _mainMenuScene;
    [SerializeField] private KeyCode _openMenuKeyCode = KeyCode.Escape;

    private void Start()
    {
        EventsManager.instance.OnOpenMenu += OnOpenMenu;
        if (_menu == null) _menu = GameObject.Find("Menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(_openMenuKeyCode))
        {
            EventsManager.instance.EventOpenMenu(!_menu.activeSelf);
        }
    }

    void OnOpenMenu(bool isOpen)
    {
        _menu.SetActive(isOpen);
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
    }

    public void ExitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_mainMenuScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
