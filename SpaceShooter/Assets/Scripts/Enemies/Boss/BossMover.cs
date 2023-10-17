using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMover : MonoBehaviour
{
    //Movement
    public float moveSpeed = 1f;
    private Vector3 plusPos = new Vector3(0,0,0);
    
    //Where to stop
    private Vector2 stopThereVector;
    private float stopThere;
    
    private void Start()
    {
        stopThereVector = Camera.main.ViewportToWorldPoint(new Vector2(0.5f,0.8f));
        stopThere = stopThereVector.y;

        StartCoroutine(StartMovingTowards());
    }

    private IEnumerator StartMovingTowards()
    {
        while (new Vector2(transform.position.x, transform.position.y) != stopThereVector)
        {
            Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(currPos, stopThereVector,moveSpeed*Time.deltaTime);
            
            yield return null;
        }
        
        if ((transform.position.y > stopThere - 0.1f) && (transform.position.y < stopThere + 0.1f))
        {
            StartCoroutine(GetComponent<BossEnemySpawner>().StartSpawning());
        }
        
        yield return null;
    }
    
    private void Update()
    {
        //Height Y move
        // plusPos.y = -moveSpeed * Time.deltaTime;
        //
        // if (!(transform.position.y > stopThere - 0.1f && transform.position.y < stopThere + 0.1f))
        // {
        //     transform.position += plusPos;
        // }

        // Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        //
        // transform.position = Vector2.MoveTowards(currPos, stopThereVector,moveSpeed*Time.deltaTime);
        //
        // if ((transform.position.y > stopThere - 0.1f) && (transform.position.y < stopThere + 0.1f))
        // {
        //     StartCoroutine(GetComponent<BossEnemySpawner>().StartSpawning());
        // }
        
    }
    
    
    
    
    
}
