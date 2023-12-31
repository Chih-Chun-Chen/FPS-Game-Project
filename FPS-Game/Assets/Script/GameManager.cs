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
    public TextMeshProUGUI timer;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI currentScoreText;

    private bool isGameOver = false;
    private float targetDeleteTime;
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
        gameTimer = MainManager.Instance.userTime;
        TimeManager(false);
        topScoreText.text = "TOP SCORE: " + MainManager.Instance.topScore;
        titleScreen.gameObject.SetActive(true);
        scoreSystem.gameObject.SetActive(false);
        StartGame();
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
            MainManager.Instance.numOfTarget++;
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
            MainManager.Instance.numOfTarget++;
            yield return new WaitForSeconds(targetDeleteTime);
            Destroy(newTarget);
        }
    }

    private void hitTargets()
    {
        if (hitTarget)
        {
            MainManager.Instance.numOfHit++;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnCoroutine = StartCoroutine(SpawnAndDeleteTargetCoroutine());
            hitTarget = false;
            UpdateScore();
        }
    }

    public void StartGame()
    {
        Cursor.visible = false;
        spawnCoroutine = StartCoroutine(SpawnAndDeleteTarget());
        isGameOver = false;
        TimeManager(true);

        switch (MainManager.Instance.userLevel)
        {
            case 0:
                targetDeleteTime = 3;
                break;
            case 1:
                targetDeleteTime = 2;
                break;
            case 2:
                targetDeleteTime = 1;
                break;
            default:
                // Handle other cases if necessary
                break;
        }

        score = -1;
        UpdateScore();
        titleScreen.gameObject.SetActive(false);
        scoreSystem.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        isGameOver = true;
        timer.gameObject.SetActive(false);
        scoreSystem.gameObject.SetActive(false);
        StopCoroutine(spawnCoroutine);
        if (score > MainManager.Instance.topScore)
        {
            MainManager.Instance.topScore = score;
        }
        TimeManager(false);
        SceneManager.LoadScene(2);
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
