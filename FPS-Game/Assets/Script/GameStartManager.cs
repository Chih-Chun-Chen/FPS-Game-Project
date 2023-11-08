using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStartManager : MonoBehaviour
{
    public GameObject mainPage;
    public GameObject settingMenu;
    public Button startBtn;
    public Button settingtBtn;
    public Button exitBtn;
    public TextMeshProUGUI userLevelText;
    public TextMeshProUGUI userDPIText;
    public Slider dpiSlider;
    public TMP_Dropdown levelDropdown;

    // Start is called before the first frame update
    void Start()
    {
        startBtn.onClick.AddListener(StartGame);
        settingtBtn.onClick.AddListener(Setting);
        exitBtn.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        MainManager.Instance.userDPI = (int)dpiSlider.value;
        MainManager.Instance.userLevel = levelDropdown.value;
        userDPIText.text = "USER DPI: " + MainManager.Instance.userDPI;
        userLevelText.text = "Level: " + levelDropdown.options[MainManager.Instance.userLevel].text;
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void Setting()
    {
        mainPage.SetActive(false);
        settingMenu.SetActive(true);
    }

    void Exit()
    {
        mainPage.SetActive(true);
        settingMenu.SetActive(false);
    }
}
