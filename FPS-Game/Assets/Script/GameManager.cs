using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameObjectsToFreeze;
    public bool hitTarget = false;
    public GameObject target;
    public GameObject titleScreen;
    public GameObject scoreSystem;
    public GameObject gameOvercreen;
    public Button restartButton;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI numOfTargetText;
    public TextMeshProUGUI numOfHitText;
    public TextMeshProUGUI numOfBulletText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI targetEngagementText;
    public int numOfBullet;

    private bool isGameOver = false;
    private float targetDeleteTime;
    private static int topScore;
    private float gameTimer;
    private int score;
    private int numOfTarget;
    private int numOfHit;
    private float accuracy;
    private float targetEngagement;
    private Coroutine spawnCoroutine;
    private Vector3 minSpawnPosition = new Vector3(-90, -58, 140);
    private Vector3 maxSpawnPosition = new Vector3(89, 122, 351);

    private void Start()
    {
        gameTimer = 60f;
        numOfTarget = 0;
        numOfHit = 0;
        numOfBullet = 0;
        TimeManager(false);
        topScoreText.text = "TOP SCORE: " + topScore;
        titleScreen.gameObject.SetActive(true);
        scoreSystem.gameObject.SetActive(false);
        gameOvercreen.gameObject.SetActive(false);
    }

    private void Update()
    {
        hitTargets();
        if (!isGameOver && gameTimer > 0f)
        {
            // Decrease the timer by Time.deltaTime
            gameTimer -= Time.deltaTime;
            // Update the timer text
            timer.text = "TIMER: " + Mathf.Round(gameTimer).ToString();
        }
        // Check if the timer has reached 0
        if (gameTimer <= 0f)
        {
            GameOver();
        }

    }

    private IEnumerator SpawnAndDeleteTarget()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
                Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
            );

            GameObject newTarget = Instantiate(target, spawnPosition, Quaternion.identity);
            numOfTarget++;
            yield return new WaitForSeconds(targetDeleteTime);
            Destroy(newTarget);
        }
    }

    private IEnumerator SpawnAndDeleteTargetCoroutine()
    {
        while (true)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
                Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
            );

            GameObject newTarget = Instantiate(target, spawnPosition, Quaternion.identity);
            numOfTarget++;
            yield return new WaitForSeconds(targetDeleteTime);
            Destroy(newTarget);
        }
    }

    private void hitTargets()
    {
        if (hitTarget)
        {
            numOfHit++;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnCoroutine = StartCoroutine(SpawnAndDeleteTargetCoroutine());
            hitTarget = false;
            UpdateScore();
        }
    }

    public void StartGame(int difficulty)
    {
        Cursor.visible = false;
        spawnCoroutine = StartCoroutine(SpawnAndDeleteTarget());
        isGameOver = false;
        TimeManager(true);
        targetDeleteTime = difficulty;
        score = -1;
        UpdateScore();
        titleScreen.gameObject.SetActive(false);
        scoreSystem.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        isGameOver = true;
        restartButton.gameObject.SetActive(true);
        gameOvercreen.gameObject.SetActive(true);
        timer.gameObject.SetActive(false);
        scoreSystem.gameObject.SetActive(false);
        StopCoroutine(spawnCoroutine);
        if (score > topScore)
        {
            topScore = score;
        }
        AnalyzeData();
        TimeManager(false);
    }

    private void UpdateScore()
    {
        score++;
        currentScoreText.text = "CURRENT SCORE: " + score;
    }

    private void TimeManager(bool setActive)
    {
        foreach (GameObject gameObject in gameObjectsToFreeze)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(setActive); // Deactivate the game object
            }
        }
        if (setActive)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    private void AnalyzeData()
    {
        accuracy = ((float)numOfHit / (float)numOfBullet) * 100f;
        targetEngagement = ((float)numOfHit / (float)numOfTarget) * 100f;

        numOfTargetText.text = "Number of target  generated: " + numOfTarget;
        numOfHitText.text = "Number of target  hit: " + numOfHit;
        numOfBulletText.text = "Number of bullet  use: " + numOfBullet;
        accuracyText.text = "Hit accuracy: " + accuracy.ToString("F2") + "%";
        targetEngagementText.text = "Target Engagement Efficiency: " + targetEngagement.ToString("F2") + "%";

    }
}
