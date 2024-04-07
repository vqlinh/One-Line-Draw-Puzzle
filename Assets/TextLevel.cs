using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLevel : MonoBehaviour
{
    private GameObject panelLevel;
    public TextMeshProUGUI txtNumberLv;
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
