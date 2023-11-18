using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawner;
    public List<GameObject> enemy;
    public Queue<GameObject> pool = new Queue<GameObject>();
    public GameObject enemyBoss;
    private Coroutine enemySpawn;

    private Vector2 xBounds;

    [SerializeField] private float spawnTime = 1f;

    private PlayerScript player;

    [Header("Boss stuff")] 
    [SerializeField] private bool isBossOn = false;
    
    private void Start()
    {
        player = PlayerScript.Instance;
        xBounds = new Vector2(player.minBounds.x, player.maxBounds.x);

        StartCoroutine(FillPool());
    }

    public void SwitchEnemySpawn(bool on)
    {
        if (on)
        {
            enemySpawn = StartCoroutine(SpawnEnemies());
        }
        else
        {
            StopCoroutine(enemySpawn);
        }
    }

    public IEnumerator SpawnEnemies()
    {
        bool spawning = true;
        
        while (spawning)
        {
            float xPos = Random.Range(xBounds.x, xBounds.y);
            
            int enemyNo = Random.Range(0, enemy.Count);
            GameObject newEnemy = enemy[enemyNo];

            if (isBossOn)
            {
                SpawnEnemyFromPool();
            }
            else
            {
                GameObject enem = Instantiate(newEnemy);
                SetEnemySpawnPosition(enem);
                enem.GetComponent<EnemyMover>().skipX = isBossOn;
            }
            
            
            yield return new WaitForSeconds(spawnTime);
            
            yield return null;
        }

    }

    public void SpawnBoss()
    {
        GameObject boss = Instantiate(enemyBoss, spawner);
        boss.GetComponent<EnemyDamageDealer>().isBossProperty = true;
    }

    private IEnumerator FillPool()
    {
        while (pool.Count < 100)
        {
            yield return null;

            int enemyNo = Random.Range(0, enemy.Count);
            GameObject newEnemy = enemy[enemyNo];
            
            GameObject enem = Instantiate(newEnemy);
            SetEnemySpawnPosition(enem);
            pool.Enqueue(enem);
            enem.gameObject.SetActive(false);
            //yield return new WaitForSeconds(0.2f);
        }
    }

    public void SetEnemySpawnPosition(GameObject enemyToMove)
    {
        float xPos = Random.Range(xBounds.x, xBounds.y);
        enemyToMove.transform.position = new Vector3(xPos, spawner.position.y, 0);
    }

    private void SpawnEnemyFromPool()
    {
        GameObject currEnemy = pool.Dequeue();
        currEnemy.SetActive(true);
        currEnemy.GetComponent<EnemyMover>().skipX = isBossOn;
    }
    
    public IEnumerator HardcoreMode()
    {
        //Save Regular Mode
        float currentFireRate = player.GetComponent<PlayerShoot>().fireRate;
        player = PlayerScript.Instance;
        float regularSpawnTime = spawnTime;
        
        //Load Hardcore mode
        Debug.Log("zaladowano");
        isBossOn = true;
        spawnTime = 0.02f;
        player.GetComponent<PlayerShoot>().gunState = PlayerShoot.GunState.Hardcore;
        player.GetComponent<PlayerShoot>().fireRate = 0.1f;

        //Balance Hardcore Mode
        yield return new WaitForSeconds(2f);
        Debug.Log("zbalansowano");
        spawnTime = 0.1f;

        yield return new WaitForSeconds(10f);
        Debug.Log("utrzymano");
        spawnTime = regularSpawnTime;

        //Hold Hardcore Mode
        yield return new WaitForSeconds(3f);
        Debug.Log("regular loaded");
        
        //Load Regular Mode
        isBossOn = false;
        player.GetComponent<PlayerShoot>().gunState = PlayerShoot.GunState.PresetTwo;
        player.GetComponent<PlayerShoot>().fireRate = currentFireRate;

        yield return null;
    }
}
