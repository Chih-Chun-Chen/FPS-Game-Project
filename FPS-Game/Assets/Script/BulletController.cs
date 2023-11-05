using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager != null && gameManager.hitTarget)
        {
            // Your logic here if hitTarget is true
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Destroy(gameObject); // Destroy the current GameObject
            Destroy(collision.gameObject); // Destroy the collided GameObject
            if (gameManager != null)
            {
                gameManager.hitTarget = true; // Set hitTarget to true in GameManager
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy the current GameObject
            Debug.Log("Hit the wall");
        }
    }
}
