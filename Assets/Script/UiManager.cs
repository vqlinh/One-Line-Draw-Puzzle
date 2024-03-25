using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

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
}
