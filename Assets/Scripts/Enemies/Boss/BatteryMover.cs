using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BatteryMover : MonoBehaviour
{
    public float batterySpeed = 1f;
    private Vector2 borders;
    private Coroutine moveCor;
    private void Start()
    {
        Vector2 currLimits = Camera.main.ViewportToWorldPoint(new Vector2(0,-0.15f));
        borders = new Vector2(transform.position.x, currLimits.y);

        moveCor = StartCoroutine(MoveMe());
    }

    private IEnumerator MoveMe()
    {
        while (new Vector2(transform.position.x,transform.position.y) != borders)
        {
            transform.position = Vector2.MoveTowards(transform.position, borders, batterySpeed * Time.deltaTime);
            yield return null;
        }
        
        HideBattery();
        
    }

    public void HideBattery()
    {
        StopCoroutine(moveCor);
        transform.position = new Vector2(10,30);
    }
}
