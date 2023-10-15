using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Display Time")] 
    public TextMeshProUGUI timeLeft;
    private float time = 60f;

    private void Start()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while (time > 0)
        {
            time -= Time.deltaTime;

            int timeDisplay = Mathf.CeilToInt(time);
            timeLeft.text = timeDisplay.ToString();
            
            yield return null;
        }
        yield return null;
    }
    
}
