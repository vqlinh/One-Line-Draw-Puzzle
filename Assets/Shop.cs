using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    int numberHint;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();

        numberHint = PlayerPrefs.GetInt("NumberHint",5);
        Save();
    }
    public void BuyHint(int hint)
    {
        numberHint += hint;
        Save();
        AudioManager.Instance.AudioBought();
    }

    void Save()
    {
        PlayerPrefs.SetInt("NumberHint", numberHint);
        PlayerPrefs.Save();
    }
}
