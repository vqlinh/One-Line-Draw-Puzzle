using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    public GameObject objectToToggle; 
    public string playerPrefsKey; 

    private bool isObjectActive = true; 

    private void Start()
    {
        LoadToggleState();
    }

    public void Toggle()
    {
        isObjectActive = !isObjectActive;
        objectToToggle.SetActive(isObjectActive);

        SaveToggleState();
    }

    private void SaveToggleState()
    {
        int stateToSave = isObjectActive ? 1 : 0;
        PlayerPrefs.SetInt(playerPrefsKey, stateToSave);
        PlayerPrefs.Save();
    }

    private void LoadToggleState()
    {
        int savedState = PlayerPrefs.GetInt(playerPrefsKey, 1); 
        isObjectActive = savedState == 1;
        objectToToggle.SetActive(isObjectActive);
    }
}
