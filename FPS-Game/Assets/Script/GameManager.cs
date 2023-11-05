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

    private bool isGameOver = false;
    private float targetDeleteTime;
    private static int topScore;
    private float gameTimer = 10f;
    private int score;
    private Coroutine spawnCoroutine;
    private Vector3 minSpawnPosition = new Vector3(-90, -58, 140);
    private Vector3 maxSpawnPosition = new Vector3(89, 122, 351);

    private void Start()
    {
        TimeManager(false);
        topScoreText.text = "TOP SCORE: " + topScore;
        titleScreen.gameObject.SetActive(true);
        scoreSystem.gameObject.SetActive(false);
        gameOvercreen.gameObject.SetActive(false);
        spawnCoroutine = StartCoroutine(SpawnAndDeleteTarget());
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
            yield return new WaitForSeconds(targetDeleteTime);
            Destroy(newTarget);
        }
    }

    private void hitTargets()
    {
        if (hitTarget)
        {
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
}
