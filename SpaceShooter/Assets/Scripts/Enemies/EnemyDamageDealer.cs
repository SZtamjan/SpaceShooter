using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] private bool isBoss = false;
    [SerializeField] private int enemyHP = 10;
    [SerializeField] private int enemyDamage = 5;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            print("trigger z graczem");
            other.gameObject.GetComponent<PlayerDamageDealer>().DealDamage(enemyDamage);
            EnemyDie();
        }
            
    }

    public bool isBossProperty
    {
        set
        {
            isBoss = value;
        }
    }

    public void DealDamage(int damage)
    {
        enemyHP -= damage;
        if (enemyHP <= 0)
        {
            if (isBoss)
            {
                BossDie();
            }
            else
            {
                EnemyDie();
            }
        }
    }

    private void BossDie()
    {
        //Update Score
        GameManager.Instance.UpdateScore(5);
        SpawnBoost();
        Destroy(gameObject);
    }

    private void EnemyDie()
    {
        //Update Score
        GameManager.Instance.UpdateScore(1);
        Destroy(gameObject);
    }

    private void SpawnBoost()
    {
        Instantiate(GameManager.Instance.boost, transform.position, Quaternion.identity);
    }
    
}
