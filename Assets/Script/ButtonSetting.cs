using UnityEngine;
using UnityEngine.UI;

public class ButtonSetting : MonoBehaviour
{
    public GameObject objectToToggle; // Tham chiếu đến game object cần bật/tắt
    public string playerPrefsKey; // Key để lưu trạng thái của setting

    private bool isObjectActive = true; // Trạng thái ban đầu

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
        int savedState = PlayerPrefs.GetInt(playerPrefsKey, 1); // Giá trị mặc định là bật (1)
        isObjectActive = savedState == 1;
        objectToToggle.SetActive(isObjectActive);
    }
}
