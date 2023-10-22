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
    public bool skipX = false;
    
    [Header("Am I From Boss")]
    public bool isFromBoss = false;
    public float whereToGoX;
    
    private void Start()
    {
        PlayerScript mov = PlayerScript.Instance;
        xLimits = new Vector2(mov.minBounds.x, mov.maxBounds.x);
        yLimit = mov.minBounds.y;
        
        GetRandomWidth();
        
        if (isFromBoss)
        {
            StartCoroutine(EnemyFromBoss());
        }
        else
        {
            StartCoroutine(RegularEnemy());
        }
    }

    private IEnumerator EnemyFromBoss()
    {
        while (transform.position.x != whereToGoX)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(whereToGoX,transform.position.y), moveSpeed*Time.deltaTime);
            yield return null;
        }

        bool goDown = true;
        while (goDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, yLimit),
                moveSpeed * Time.deltaTime);
            if (transform.position.y <= yLimit)
            {
                Destroy(gameObject);
                goDown = false;
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator RegularEnemy()
    {
        bool moveDown = true;
        while (moveDown)
        {
            //Height Y move
            plusPos.y = -moveSpeed * Time.deltaTime;

            if (!skipX)
            {
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
            }
            
        
            //Set new position
            transform.position += plusPos;
        
            //Destroy if far away
            if (transform.position.y < yLimit - 1f)
            {
                Destroy(gameObject);
                moveDown = false;
            }

            yield return null;
        }

        yield return null;
    }
    
    
    
    private void GetRandomWidth()
    {
        xPos = Random.Range(xLimits.x, xLimits.y);
    }
    
}
