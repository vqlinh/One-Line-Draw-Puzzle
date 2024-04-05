using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLevel : MonoBehaviour
{
    private GameObject panelLevel;
    //int number;
    public TextMeshProUGUI txtNumberLv;
    private void Start()
    {
        panelLevel = GameObject.Find("PanelLevel");
        panelLevel.SetActive(false);
        //number = PlayerPrefs.GetInt("SelectedLevel");
    }
    private void Update()
    {
        txtNumberLv.text = LevelButton.Instance.nextLevel.ToString();
    }

}
