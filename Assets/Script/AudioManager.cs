using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;
    public AudioClip buttonClick;
    public AudioClip win;
    public AudioClip pointTouch;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
