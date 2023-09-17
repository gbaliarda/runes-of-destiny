using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _victory;
    [SerializeField] private AudioClip _defeat;
    [SerializeField] private AudioClip _background;

    private AudioSource _audioSource;

    #region UNITY_EVENTS
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        EventsManager.instance.OnGameOver += OnGameOver;
        _audioSource.PlayOneShot(_background);
    }
    #endregion

    #region EVENTS 
    private void OnGameOver(bool isVictory)
    {
        _audioSource.PlayOneShot(isVictory ? _victory : _defeat);
    }
    #endregion
}
