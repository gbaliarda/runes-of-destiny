using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePotionSound : MonoBehaviour, IListener
{
    #region PUBLIC_PROPERTIES
    public AudioClip AudioClip => _audioClip;

    public AudioSource AudioSource => _audioSource;
    #endregion

    #region PRIVATE_PROPERTIES
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;

    #endregion
    public void InitAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        //
    }

    public void PlayOneShot()
    {
        if (_audioSource != null && _audioClip != null) _audioSource.PlayOneShot(_audioClip);
    }

    public void Stop()
    {
        //
    }
    void Start()
    {
        InitAudioSource();
    }
}
