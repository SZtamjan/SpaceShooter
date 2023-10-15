using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    [SerializeField] private int playerHP = 50;

    public void DealDamage(int dmg)
    {
        playerHP -= dmg;
        CheckIfDead();
    }

    public void HealPlayer(int heal)
    {
        playerHP += heal;
    }

    private void CheckIfDead()
    {
        if (playerHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Do something
        Destroy(gameObject);
    }
    
}
