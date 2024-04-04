using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLevel : MonoBehaviour
{
    public TextMeshProUGUI txtNumberLv;
    private void Update()
    {
        txtNumberLv.text = LevelButton.Instance.nextLevel.ToString();
    }

}
