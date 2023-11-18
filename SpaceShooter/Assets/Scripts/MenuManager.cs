using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    private UIControllerMenu _uiControllerMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _uiControllerMenu = UIControllerMenu.Instance;

        InitData();
    }

    private void InitData()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        string score;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            score = PlayerPrefs.GetInt("HighScore").ToString();
        }
        else
        {
            score = "No highscore yet :c";
        }
        _uiControllerMenu.LoadHighScore(score);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
    
}
