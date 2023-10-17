using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySpawner : MonoBehaviour
{
    private Camera cam;
    private GameObject localEnemy;
    [SerializeField] private List<Transform> spawnsLeft;
    [SerializeField] private List<Transform> spawnsRight;
    private Vector2 leftEdge, rightEdge = new Vector2();

    private void Start()
    {
        cam = Camera.main;
        leftEdge = cam.ViewportToWorldPoint(new Vector2(0.2f, 0));
        rightEdge = cam.ViewportToWorldPoint(new Vector2(0.8f, 0));
        
        localEnemy = GameManager.Instance.GetComponent<EnemySpawner>().enemy[1];
    }

    public IEnumerator StartSpawning()
    {
        bool inProgress = true;

        SpawnNextPreset(0);
        yield return new WaitForSeconds(1f);
            
        SpawnNextPreset(1);
        yield return new WaitForSeconds(1f);
            
        SpawnNextPreset(2);


        yield return null;
    }

    private void SpawnNextPreset(int presetNO)
    {
        GameObject leftEnemy = Instantiate(localEnemy, spawnsLeft[presetNO].position,Quaternion.identity);
        GameObject rightEnemy = Instantiate(localEnemy, spawnsRight[presetNO].position,Quaternion.identity);

        leftEnemy.GetComponent<EnemyMover>().isFromBoss = true;
        leftEnemy.GetComponent<EnemyMover>().whereToGoX = leftEdge.x;
        
        rightEnemy.GetComponent<EnemyMover>().isFromBoss = true;
        rightEnemy.GetComponent<EnemyMover>().whereToGoX = rightEdge.x;
        
        
        
    }
}
