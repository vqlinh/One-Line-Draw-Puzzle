using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public static LevelButton Instance;
    [SerializeField] private Sprite currentButton;
    [SerializeField] private Sprite playedButton;
    [SerializeField] private Sprite lockedButton;
    [SerializeField] private Image buttonImg;
    [SerializeField] private TextMeshProUGUI numberLevel;

    public int numLevel;
    public int nextLevel;
    private Button button;
    public void OnButtonClick()
    {
        // Lưu số numLevel vào PlayerPrefs
        PlayerPrefs.SetInt("SelectedLevel", numLevel);
        PlayerPrefs.Save(); // Lưu thay đổi

        // Load scene mới
        SceneManager.LoadScene("GamePlay");
    }

    private void Awake()
    {
        Assert.IsNotNull(currentButton);
        Assert.IsNotNull(playedButton);
        Assert.IsNotNull(lockedButton);
        Assert.IsNotNull(buttonImg);
        Assert.IsNotNull(numberLevel);
        Instance = this;
        numberLevel.text = numLevel.ToString();
        //nextLevel = 1;
        int nb =PlayerPrefs.GetInt("CompletedLevel");
        nextLevel = nb + 1;
        //PlayerPrefs.DeleteAll();
        //button=GetComponent<Button>();
    }

    private void Start()
    {
        Debug.Log("nextlevel : "+nextLevel);
        if (numLevel == nextLevel)
        {
            buttonImg.sprite = currentButton;

        }
        else if (numLevel < nextLevel)
        {
            buttonImg.sprite = playedButton;
        }
        else
        {
            buttonImg.sprite = lockedButton;
            numberLevel.gameObject.SetActive(false);
            button = GetComponentInChildren<Button>();

            // Khóa button nếu numLevel lớn hơn nextLevel
            button.interactable = false;
        }

    }

  
}
