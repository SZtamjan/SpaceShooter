using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControllerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private Vector2 startTouch, endTouch;
    [SerializeField] private CanvasGroup blackOut;
    [SerializeField] private Transform ship;

    private void Start()
    {
        LoadHighScore();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouch = Input.GetTouch(0).position;
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouch = Input.GetTouch(0).position;
            
            if (endTouch.y > startTouch.y)
            {
                Debug.Log("tutaj zeswipowalem do gory i pozniej przejscie do kolejnej sceny");
                FlyOutAndBlackOutStart();
            }
        }

        
    }

    private void FlyOutAndBlackOutStart()
    {
        //blackout
        blackOut.alpha = 0f;
        blackOut.DOFade(1, 1f);
        
        //flyout
        ship.DOMoveY(ship.transform.position.y + 10f, 2f, false);
        StartCoroutine(IsFullBlackOut());
    }

    private IEnumerator IsFullBlackOut()
    {
        while (blackOut.alpha < 1)
        {
            yield return null;
        }
        LoadScene();
    }

    private void LoadScene()
    {
        int newIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(newIndex);
    }

    private void LoadHighScore()
    {
        string score = "99999";
        highScoreText.text = "Highscore: " + score;
    }
    
}
