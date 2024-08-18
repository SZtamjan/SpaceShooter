using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private EnemySpawner _enemySpawnerScript;
    private PlayerScript _playerScript;
    private UIControllerGame _uiControllerGame;

    public UnityEvent gamePause;
    public UnityEvent gameResume;

    private void Awake()
    {
        Instance = this;
    }

    bool bossSpawned = false;
    private float time = 60f;
    private int totalScore = 0;

    [Header("Boost time")] public GameObject boost;

    [Header(" ")] [Header("Debug")] 
    [SerializeField] private bool apply;
    public int val;
    
    private void Start()
    {
        UpdateGameState(GameState.Initialization);
    }


    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Initialization:
                InitData();
                UnFreezePlayer();
                StartAndWaitForFadeOut();
                break;
            case GameState.FirstStage:
                StartCoroutine(StartTimer());
                StartEnemies();
                break;
            case GameState.BossStage:
                BossTime();
                break;
            case GameState.Victory:
                StopEnemies();
                FreezePlayer();
                VictoryNotification();
                SetGameEnd();
                break;
            case GameState.GameEnd:
                SaveHighScore();
                ShowEndScreen();
                print("game ended now it should take player back to the menu or sth");
                break;
            default:
                break;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePause.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameResume.Invoke();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

        private void InitData()
    {
        //assign scripts
        _enemySpawnerScript = GetComponent<EnemySpawner>();
        _playerScript = PlayerScript.Instance;
        _uiControllerGame = UIControllerGame.Instance;
        
        //set texts
        DisplayTime();
        UpdateScore(0);
        
        //Assign events
        gamePause.AddListener(_playerScript.StopPlayerMovement);
        gamePause.AddListener(_uiControllerGame.ShowPauseMenu);
        
        gameResume.AddListener(_playerScript.StartPlayerMovement);
        gameResume.AddListener(_uiControllerGame.HidePauseMenu);
        
    }

    public void StartAndWaitForFadeOut()
    {
        if (_uiControllerGame.blackOutProperty.alpha == 0)
        {
            StartFirstStage();
        }
        else
        {
            _uiControllerGame.FadeOutBlack();
            StartCoroutine(_uiControllerGame.CheckIfBlackOut());
        }
    }

    private IEnumerator StartTimer()
    {

        while (time > 0)
        {
            time -= Time.deltaTime;

            CheckIfTimeRunOut();
            CheckIfBossTime();
            
            
            //Debug - delete later
            if (apply)
            {
                time = val;
                apply = false;
            }
            
            DisplayTime();

            yield return null;
        }

        yield return null;
    }

    private void StartFirstStage()
    {
        UpdateGameState(GameState.FirstStage);
    }

    #region PlayerControl

    private void FreezePlayer()
    {
        _playerScript.SwitchPlayerMovement(true);
    }

    private void UnFreezePlayer()
    {
        _playerScript.SwitchPlayerMovement(false);
    }

    #endregion
    

    #region EnemyControl

    private void StartEnemies()
    {
        _enemySpawnerScript.SwitchEnemySpawn(true);
    }

    private void StopEnemies()
    {
        _enemySpawnerScript.SwitchEnemySpawn(false);
    }

    #endregion

    #region ActiveFunctions

    private void CheckIfBossTime()
    {
        if (time < 35f && !bossSpawned)
        {
            UpdateGameState(GameState.BossStage);
        }
    }
    
    private void BossTime()
    {
        bossSpawned = true;
        _enemySpawnerScript.SpawnBoss();
    }
    
    public void UpdateScore(int deltaScore)
    {
        totalScore += deltaScore;
        _uiControllerGame.DisplayScore(totalScore);
    }

    #endregion

    
    private void CheckIfTimeRunOut()
    {
        if (time < 0f)
        {
            UpdateGameState(GameState.Victory);
        }
    }

    private void DisplayTime()
    {
        _uiControllerGame.DisplayTime(time,false);
    }

    private void VictoryNotification()
    {
        time = 0f;
        _uiControllerGame.DisplayTime(time,true);
    }
    
    #region EndGame

    private void SetGameEnd()
    {
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5f);
        UpdateGameState(GameState.GameEnd);
    }

    private void SaveHighScore()
    {
        GetComponent<TemporarySaveSystem>().WriteHighScore(totalScore);
    }
    
    private void ShowEndScreen()
    {
        _uiControllerGame.ShowFakePauseMenu();
    }

    #endregion
    
}

public enum GameState
{
    Initialization,
    FirstStage,
    BossStage,
    Victory,
    GameEnd
}