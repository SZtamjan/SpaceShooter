using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawner;
    public List<GameObject> enemy;
    public GameObject enemyBoss;

    private Vector2 xBounds;

    [SerializeField] private float spawnTime = 1f;

    private PlayerScript player;

    [Header("Boss stuff")] 
    [SerializeField] private bool isBossOn = false;
    
    private void Start()
    {
        player = PlayerScript.Instance;
        xBounds = new Vector2(player.minBounds.x, player.maxBounds.x);
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        bool spawning = true;
        
        while (spawning)
        {
            float xPos = Random.Range(xBounds.x, xBounds.y);
            
            int enemyNo = Random.Range(0, enemy.Count);
            GameObject newEnemy = enemy[enemyNo];
                
            GameObject enem = Instantiate(newEnemy, new Vector3(xPos, spawner.position.y, 0),Quaternion.identity);
            enem.GetComponent<EnemyMover>().skipX = isBossOn;
            
            yield return new WaitForSeconds(spawnTime);
            
            yield return null;
        }

    }

    public void SpawnBoss()
    {
        GameObject boss = Instantiate(enemyBoss, spawner);
        boss.GetComponent<EnemyDamageDealer>().isBossProperty = true;
    }
    
   
    
    public IEnumerator HardcoreMode()
    {
        //Save Regular Mode
        float currentFireRate = player.GetComponent<PlayerShoot>().fireRate;
        player = PlayerScript.Instance;
        float regularSpawnTime = spawnTime;
        
        //Load Hardcore mode
        isBossOn = true;
        spawnTime = 0.02f;
        player.GetComponent<PlayerShoot>().gunState = PlayerShoot.GunState.Hardcore;
        player.GetComponent<PlayerShoot>().fireRate = 0.1f;

        //Balance Hardcore Mode
        yield return new WaitForSeconds(2f);
        spawnTime = 0.1f;

        yield return new WaitForSeconds(10f);
        spawnTime = regularSpawnTime;

        //Hold Hardcore Mode
        yield return new WaitForSeconds(3f);
        
        //Load Regular Mode
        isBossOn = false;
        player.GetComponent<PlayerShoot>().gunState = PlayerShoot.GunState.PresetTwo;
        player.GetComponent<PlayerShoot>().fireRate = currentFireRate;

        yield return null;
    }
}
