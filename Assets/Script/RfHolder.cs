using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class RfHolder : Singleton<RfHolder>
{
    private GameObject panelLevel;
    public TextMeshProUGUI txtNumberLv;
    public GameObject panel;
    private void Start()
    {
        panelLevel = GameObject.Find("PanelLevel");
        panelLevel.SetActive(false);
    }
    private void Update()
    {
        txtNumberLv.text = LevelButton.Instance.nextLevel.ToString();
    }
}
