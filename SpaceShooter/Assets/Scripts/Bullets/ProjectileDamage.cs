using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private int damage;

    public int ProjectileDamageProperty
    {
        get
        {
            return damage; // Pobieranie wartości właściwości.
        }
        set
        {
            // Tutaj możesz dodać swoją logikę lub walidację.
            if (value >= 0) // Przykładowa walidacja (wartość musi być nieujemna).
            {
                damage = value; // Przypisanie wartości do prywatnego pola.
            }
            else
            {
                Debug.LogError("Wartość musi być nieujemna.");
            }
        }
    }
    
    
    
}
