using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private int damage;

    public int ProjectileDamageProperty
    {
        set
        {
            if (value >= 0)
            {
                damage = value;
            }
            else
            {
                Debug.LogError("Value can't be less than 0.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDamageDealer>().DealDamage(damage);
            Explode();
        }

        
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
    
}
