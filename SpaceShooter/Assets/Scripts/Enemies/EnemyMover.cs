using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyMover : MonoBehaviour
{
    private EnemySpawner _enemySpawner;

    public EnemySpawner EnemySpawnerProp
    {
        get => _enemySpawner;
    }
    
    public float moveSpeed = 1f;
    private Vector3 plusPos = new Vector3(0,0,0);
    private Vector2 xLimits;
    private float yLimit;
    

    public float xPos;
    public bool skipX = false;
    
    [Header("Am I From Boss")]
    public bool isFromBoss = false;
    public float whereToGoX;
    private Coroutine cor;


    public int a, b, c, d = 0;
    
    private void Start()
    {
        InitData();
    }

    private void InitData()
    {
        _enemySpawner = GameManager.Instance.GetComponent<EnemySpawner>();
        PlayerScript mov = PlayerScript.Instance;
        xLimits = new Vector2(mov.minBounds.x, mov.maxBounds.x);
        yLimit = mov.minBounds.y;
        plusPos = new Vector3(0,0,0);
        
        GetRandomWidth();
    }

    private void CorMan()
    {
        if(cor == null) cor = StartCoroutine(RegularEnemy());
    }

    public void StartEnemiesFromBoss()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(EnemyFromBoss());
    }

    public void StartRegularEnemies() //Implement someday - delete CorMan
    {
        if (cor != null)
        {
            StopCoroutine(cor);
            cor = null;
        }
        cor = StartCoroutine(RegularEnemy());
    }

    private void OnEnable()
    {
        InitData();
        CorMan();
    }

    private void OnDisable()
    {
        if(cor != null) StopCoroutine(cor);
        cor = null;
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
            plusPos.x = 0;

            a++;

            if (!skipX)
            {
                //Width X move
                if ((xPos >= transform.position.x-0.1f) && (xPos <= transform.position.x+0.1f))
                {
                    GetRandomWidth();
                    b++;
                }

                if (xPos > transform.position.x)
                {
                    plusPos.x = moveSpeed * Time.deltaTime;
                    c++;
                }
        
                if (xPos < transform.position.x)
                {
                    plusPos.x = -moveSpeed * Time.deltaTime;
                    d++;
                }
            }
            
        
            //Set new position
            transform.position += plusPos;
        
            //Destroy if far away
            if (transform.position.y < yLimit - 1f)
            {
                _enemySpawner.pool.Enqueue(gameObject);
                _enemySpawner.SetEnemySpawnPosition(gameObject);
                gameObject.SetActive(false);
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
