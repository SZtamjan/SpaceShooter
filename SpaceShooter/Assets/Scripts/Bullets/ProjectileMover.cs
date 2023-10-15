using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    //Move
    public float moveSpeed = 3f;
    private Vector3 plusPos = new Vector3();
    
    //Projectile border
    private float yLimit;

    private void Start()
    {
        PlayerScript mov = PlayerScript.Instance;
        yLimit = mov.minBounds.y;
        yLimit = -yLimit;
    }

    private void Update()
    {
        plusPos.y = moveSpeed * Time.deltaTime;
        transform.position += plusPos;
        
        if (transform.position.y > yLimit + 1f)
        {
            Destroy(gameObject);
        }
    }
    
    
}
