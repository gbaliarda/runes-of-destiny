using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _victory;
    [SerializeField] private GameObject _defeat;
    void Start()
    {
        gameObject.SetActive(false);
        EventsManager.instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool isVictory)
    {
        gameObject.SetActive(true);
        _victory.SetActive(isVictory);
        _defeat.SetActive(!isVictory);
    }
}
