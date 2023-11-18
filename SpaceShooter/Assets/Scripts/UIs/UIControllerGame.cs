using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIControllerGame : MonoBehaviour
{
    public static UIControllerGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    private GameManager _gameManager;
    
    [Header("Displays")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI notificationText;
    
    
    [Header("Others")]
    [SerializeField] private CanvasGroup blackOut;
    
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuButton;
    [SerializeField] private GameObject closeMenuButton;
    [SerializeField] private GameObject pauseMenu;

    public CanvasGroup blackOutProperty
    {
        get => blackOut;
    }
    
    private Vector2 startTouch, endTouch, swipeBorder;
    private Camera mainCam;

    private void Start()
    {
        _gameManager = GameManager.Instance;

        mainCam = Camera.main;
        swipeBorder = mainCam.ViewportToWorldPoint(new Vector2(0,0.2f));
        
        notificationText.text = "";
        pauseMenu.SetActive(false);
    }

    public void FadeOutBlack()
    {
        //black fade out
        blackOut.alpha = 1f;
        blackOut.DOFade(0, 1f);
    }

    public IEnumerator CheckIfBlackOut()
    {
        while (true)
        {
            if (blackOut.alpha == 0)
            {
                _gameManager.StartAndWaitForFadeOut();
                break;
            }
            yield return null;
        }
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseMenuButton.SetActive(false);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        pauseMenuButton.SetActive(true);
    }

    public void ShowFakePauseMenu() //this is end screen - modified pause menu, just with removed close button
    {
        pauseMenuButton.SetActive(false);
        pauseMenu.SetActive(true);
        closeMenuButton.SetActive(false);
    }

    public void ResetGameButton()
    {
        _gameManager.ResetGame();
    }

    public void ReturnToMenuButton()
    {
        _gameManager.ReturnToMenu();
    }

    public void DisplayTime(float timeFloat, bool win)
    {
        string timeString = timeFloat.ToString("F2", CultureInfo.InvariantCulture);
        if (win)
        {
            timerText.text = timeString;
            notificationText.text = "Winn!!1!";
            notificationText.gameObject.SetActive(true);
        }
        else
        {
            timerText.text = timeString;
        }
    }

    public void DisplayScore(int score)
    {
        scoreText.text = score.ToString();
    }

}
