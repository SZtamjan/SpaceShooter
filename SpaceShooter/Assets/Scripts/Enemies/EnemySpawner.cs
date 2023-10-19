using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    private void Start()
    {
        player = PlayerScript.Instance;
        xBounds = new Vector2(player.minBounds.x, player.maxBounds.x);
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        int i = 0;
        bool spawning = true;
        
        while (spawning)
        {
            float xPos = Random.Range(xBounds.x, xBounds.y);
            
            int enemyNo = Random.Range(0, enemy.Count);
            GameObject newEnemy = enemy[enemyNo];

            Instantiate(newEnemy, new Vector3(xPos, spawner.position.y, 0),Quaternion.identity);
            i++;

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
        float regularSpawnTime = spawnTime;
        
        //Load Hardcore mode
        spawnTime = 0.2f;
        player.Thor(true);
        
        //Hold Hardcore Mode
        yield return new WaitForSeconds(15f);
        
        //Load Regular Mode
        player.Thor(false);
        player.GetComponent<PlayerShoot>().fireRate = 1f;
        spawnTime = regularSpawnTime;
        
        yield return null;
    }
    
}
