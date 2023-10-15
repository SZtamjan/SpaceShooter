using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private int enemyHP = 10;
    [SerializeField] private int enemyDamage = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger z graczem");
        other.gameObject.GetComponent<PlayerDamageDealer>().DealDamage(enemyDamage);
    }
}
