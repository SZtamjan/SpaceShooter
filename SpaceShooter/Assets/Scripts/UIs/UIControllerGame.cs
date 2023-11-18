using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIControllerGame : MonoBehaviour
{
    public static UIControllerGame Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    [SerializeField] private CanvasGroup blackOut;
    
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuButton;
    [SerializeField] private GameObject pauseMenu;

    public CanvasGroup blackOutProperty
    {
        get => blackOut;
    }
    
    private Vector2 startTouch, endTouch, swipeBorder;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        swipeBorder = mainCam.ViewportToWorldPoint(new Vector2(0,0.2f));
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
            
            if (endTouch.y < swipeBorder.y && endTouch.y > startTouch.y)
            {
                Debug.Log("tutaj zeswipowalem do gory i pozniej przejscie do kolejnej sceny");
                //FlyOutAndBlackOutStart();
            }
        }
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
                GameManager.Instance.StartAndWaitForFadeOut();
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
    
}
