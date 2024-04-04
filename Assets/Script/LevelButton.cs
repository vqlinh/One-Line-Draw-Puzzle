using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
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

    private void Awake()
    {
        Assert.IsNotNull(currentButton);
        Assert.IsNotNull(playedButton);
        Assert.IsNotNull(lockedButton);
        Assert.IsNotNull(buttonImg);
        Assert.IsNotNull(numberLevel);
        Instance = this;
    }

    private void Start()
    {
        numberLevel.text = numLevel.ToString();

        nextLevel = PlayerPrefs.GetInt("next_level", 3);
        if (numLevel == nextLevel)
        {
            buttonImg.sprite = currentButton;

        }
        else if (numLevel < nextLevel)
        {
            buttonImg.sprite = playedButton;
            numberLevel.gameObject.SetActive(false);
        }
        else
        {
            buttonImg.sprite = lockedButton;
            numberLevel.gameObject.SetActive(false);
        }

    }

  
}
