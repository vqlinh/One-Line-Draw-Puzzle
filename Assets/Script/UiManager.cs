using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : Singleton<UiManager>
{

    public void LightVib()
    {
        HapticFeedback.LightFeedback();
        Debug.Log("LightVib");
    }
    public void MediumVib()
    {
        HapticFeedback.MediumFeedback();
    }
    public void HeavyVib()
    {
        HapticFeedback.HeavyFeedback();
        Debug.Log("HeavyVib");
    }




    public void LoadSceneGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    } 
    public void LoadSceneHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
