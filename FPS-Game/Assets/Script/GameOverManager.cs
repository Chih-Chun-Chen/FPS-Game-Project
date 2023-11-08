using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI numOfTargetText;
    public TextMeshProUGUI numOfHitText;
    public TextMeshProUGUI numOfBulletText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI targetEngagementText;
    public Button restartButton;
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        AnalyzeData();
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AnalyzeData()
    {
        MainManager.Instance.accuracy = ((float)MainManager.Instance.numOfHit / (float)MainManager.Instance.numOfBullet) * 100f;
        MainManager.Instance.targetEngagement = ((float)MainManager.Instance.numOfHit / (float)MainManager.Instance.numOfTarget) * 100f;

        numOfTargetText.text = "Number of target  generated: " + MainManager.Instance.numOfTarget;
        numOfHitText.text = "Number of target  hit: " + MainManager.Instance.numOfHit;
        numOfBulletText.text = "Number of bullet  use: " + MainManager.Instance.numOfBullet;
        accuracyText.text = "Hit accuracy: " + MainManager.Instance.accuracy.ToString("F2") + "%";
        targetEngagementText.text = "Target Engagement Efficiency: " + MainManager.Instance.targetEngagement.ToString("F2") + "%";
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
        MainManager.Instance.numOfHit = 0;
        MainManager.Instance.numOfBullet = 0;
        MainManager.Instance.numOfTarget = 0;
    }

    void Exit()
    {
        #if UNITY_EDITOR
                // If the game is running in the Unity Editor, stop playing the scene
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If the game is not running in the Unity Editor, quit the application
                Application.Quit();
        #endif
    }
}
