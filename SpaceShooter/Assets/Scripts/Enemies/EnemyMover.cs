using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector3 plusPos = new Vector3(0,0,0);
    private Vector2 xLimits;
    private float yLimit;
    
    private float xPos;

    private void Start()
    {
        Movement mov = Movement.Instance;
        xLimits = new Vector2(mov.minBounds.x, mov.maxBounds.x);
        yLimit = mov.minBounds.y;
        
        GetRandomWidth();
    }

    private void Update()
    {
        //Height Y move
        plusPos = new Vector3(0, -moveSpeed * Time.deltaTime, 0);
        
        //Width X move
        if ((xPos >= transform.position.x-0.1f) && (xPos <= transform.position.x+0.1f))
        {
            GetRandomWidth();
        }

        if (xPos > transform.position.x)
        {
            plusPos.x = moveSpeed * Time.deltaTime;
        }
        
        if (xPos < transform.position.x)
        {
            plusPos.x = -moveSpeed * Time.deltaTime;
        }
        
        //Set new position
        transform.position += plusPos;
        
        //Destroy if far away
        if (transform.position.y < yLimit - 1f)
        {
            Destroy(gameObject);
        }
    }

    private void GetRandomWidth()
    {
        xPos = Random.Range(xLimits.x, xLimits.y);
    }
    
}