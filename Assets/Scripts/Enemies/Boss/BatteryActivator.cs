using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryActivator : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Instance.GetComponent<EnemySpawner>().HardcoreMode());
            GetComponent<BatteryMover>().HideBattery();
        }
    }
}
