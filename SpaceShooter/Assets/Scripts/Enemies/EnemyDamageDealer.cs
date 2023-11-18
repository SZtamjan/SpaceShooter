using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [System.Serializable]
    private class EnemyStats
    {
        public bool isBoss = false;
        public int enemyHP = 10;
        public int enemyDamage = 5;
    }

    [SerializeField] private EnemyStats current, backUp;

    

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            print("trigger z graczem");
            other.gameObject.GetComponent<PlayerDamageDealer>().DealDamage(current.enemyDamage);
            EnemyDie();
        }
            
    }

    public bool isBossProperty
    {
        set
        {
            current.isBoss = value;
        }
    }

    public void DealDamage(int damage)
    {
        current.enemyHP -= damage;
        if (current.enemyHP <= 0)
        {
            if (current.isBoss)
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
        GetComponent<EnemyMover>().EnemySpawnerProp.pool.Enqueue(gameObject);
        GetComponent<EnemyMover>().EnemySpawnerProp.SetEnemySpawnPosition(gameObject);
        gameObject.SetActive(false);
    }

    private void SpawnBoost()
    {
        Instantiate(GameManager.Instance.boost, transform.position, Quaternion.identity);
    }

    public void RestoreEnemy()
    {
        current = backUp;
    }
    
}
