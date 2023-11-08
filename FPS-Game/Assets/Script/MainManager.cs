using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int userDPI;
    public int userLevel;
    public int numOfBullet;
    public int numOfTarget;
    public int numOfHit;
    public float accuracy;
    public float targetEngagement;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        numOfTarget = 0;
        numOfHit = 0;
        numOfBullet = 0;
    }
}
