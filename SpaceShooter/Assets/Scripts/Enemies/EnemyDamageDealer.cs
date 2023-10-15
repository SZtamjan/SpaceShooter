using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private int enemyHP = 10;
    [SerializeField] private int enemyDamage = 5;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        print("kolizja z graczem");
        other.gameObject.GetComponent<PlayerDamageDealer>().DealDamage(enemyDamage);
    }
}
