using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip bg;
    public AudioClip win;
    public AudioClip bought;
    public AudioClip pointTouch;
    public AudioClip buttonClick;
    public AudioSource audioSource;

    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            //PlayBackgroundMusic();
            audioSource.volume = 1f;
        }
        else
        {
            //StopAllAudio();
            audioSource.volume = 0f;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (Instance == this)
        {
            SetActive(true);
            Background();
        }
    }

    public void Background()
    {
        audioSource.clip = bg;
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void AudioButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    public void AudioWin()
    {
        audioSource.PlayOneShot(win);
    }

    public void AudioPointTouch()
    {
        audioSource.PlayOneShot(pointTouch);
    }

    public void AudioBought()
    {
        audioSource.PlayOneShot(bought);
    }
}
