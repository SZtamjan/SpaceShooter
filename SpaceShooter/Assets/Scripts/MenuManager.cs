using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private UIControllerMenu _uiControllerMenu;

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
}
