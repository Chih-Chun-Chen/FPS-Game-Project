using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    static public int userDPI;
    static public int userLevel;
    public GameObject mainPage;
    public GameObject settingMenu;
    public Button startBtn;
    public Button settingtBtn;
    public Button exitBtn;
    public TextMeshProUGUI userLevelText;
    public TextMeshProUGUI userDPIText;
    public Slider dpiSlider;
    public TMP_Dropdown levelDropdown;

    public static MainManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
        userDPI = (int)dpiSlider.value;
        userLevel = levelDropdown.value;
        userDPIText.text = "USER DPI: " + userDPI.ToString();
        userLevelText.text = "Level: " + levelDropdown.options[userLevel].text;
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
