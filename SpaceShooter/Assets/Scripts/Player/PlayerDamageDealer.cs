using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    //Do usuniecia, kloci sie z celem gry - ma sie nie da przegrac
    
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
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        //Do something
        Destroy(gameObject);
    }
    
}
