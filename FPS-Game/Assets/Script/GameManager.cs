using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameObjectsToFreeze;
    public bool hitTarget = false;
    public GameObject target;
    public GameObject titleScreen;
    public TextMeshProUGUI topScoreText;
    public TextMeshProUGUI currentScoreText;
    
    private bool isGameOver = false;
    private float targetDeleteTime;
    private int topScore;
    private int score = -1;
    private Coroutine spawnCoroutine;
    private Vector3 minSpawnPosition = new Vector3(-90, -58, 140);
    private Vector3 maxSpawnPosition = new Vector3(89, 122, 351);

    private void Start()
    {
        TimeManager(false);
        titleScreen.gameObject.SetActive(true);
        UpdateScore();
        spawnCoroutine = StartCoroutine(SpawnAndDeleteTarget());
    }

    private void Update()
    {
       hitTargets();
        Debug.Log("Difficulty " + targetDeleteTime);
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
        score = 0;
        UpdateScore();
        titleScreen.gameObject.SetActive(false);
    }

    /*
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameOver = true;
        restartButton.gameObject.SetActive(true);
    }
    */

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
