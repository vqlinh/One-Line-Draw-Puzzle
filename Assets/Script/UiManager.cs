using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using UnityEngine.SceneManagement;

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
        Debug.Log("MediumVib");
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
