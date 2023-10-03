using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    #region SINGLETON
    static public SoundManager instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    #endregion

    [SerializeField] private AudioClip _background;

    private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    #region UNITY_EVENTS
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = _background;
        _audioSource.Play();
    }
    #endregion

    #region EVENTS 

    #endregion

    public void PlayOneShot(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
