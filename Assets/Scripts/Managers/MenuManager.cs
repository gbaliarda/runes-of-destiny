using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private string _mainMenuScene;

    private void Start()
    {
        EventsManager.instance.OnOpenMenu += OnOpenMenu;
        if (_menu == null) _menu = GameObject.Find("Menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
