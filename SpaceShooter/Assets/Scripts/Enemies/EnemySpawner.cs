using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public Transform spawner;
    public List<GameObject> enemy;

    private Vector2 xBounds;
    
    

    private void Start()
    {
        Movement mov = Movement.Instance;
        xBounds = new Vector2(mov.minBounds.x, mov.maxBounds.x);
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

            Instantiate(newEnemy, new Vector3(xPos, 7, 0),Quaternion.identity);
            i++;

            yield return new WaitForSeconds(1f);
            
            yield return null;
        }

    }
}
