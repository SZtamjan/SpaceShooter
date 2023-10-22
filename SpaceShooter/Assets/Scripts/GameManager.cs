using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("TMP")]
    public bool isBossSpawnable = false;
    
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    bool bossSpawned = false;

    [Header("Display Time")] public TextMeshProUGUI timeLeft;
    private float time = 60f;

    [Header("Display Score")] public TextMeshProUGUI scoreText;
    private int totalScore = 0;

    [Header("Display Win or Lose")] public TextMeshProUGUI youWon;

    [Header("Boost time")] public GameObject boost;

    private void Start()
    {
        InitData();
        StartCoroutine(StartTimer());
    }

    private void InitData()
    {
        youWon.text = "";
        scoreText.text = totalScore.ToString();
    }

    private IEnumerator StartTimer()
    {

        while (time > 0)
        {
            time -= Time.deltaTime;

            CheckIfTimeRunOut();
            if(isBossSpawnable) CheckIfBossTime();
            

            timeLeft.text = time.ToString("F2", CultureInfo.InvariantCulture);

            yield return null;
        }

        yield return null;
    }

    private void CheckIfBossTime()
    {
        if (time < 35f && !bossSpawned)
        {
            bossSpawned = true;
            GetComponent<EnemySpawner>().SpawnBoss();
        }
    }

    private void CheckIfTimeRunOut()
    {
        if (time < 0f)
        {
            time = 0f;
            youWon.text = "Winn!!1!";
        }
    }

    public void UpdateScore(int deltaScore)
    {
        totalScore += deltaScore;
        scoreText.text = totalScore.ToString();
    }
}
