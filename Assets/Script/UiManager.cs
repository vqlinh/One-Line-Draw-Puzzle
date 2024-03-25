using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.SceneManagement;

public class UiManager : Singleton<UiManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LightVib()
    {
        HapticFeedback.LightFeedback();
        Debug.Log("LightVib");
    }
    public void MediumVib()
    {
        HapticFeedback.LightFeedback();
        Debug.Log("MediumVib");
    }
    public void HeavyVib()
    {
        HapticFeedback.LightFeedback();
        Debug.Log("HeavyVib");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
