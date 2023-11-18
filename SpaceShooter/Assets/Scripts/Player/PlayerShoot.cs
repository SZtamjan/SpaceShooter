using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.ThunderAndLightning;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameManager _gameManager;
    private EnemySpawner _enemySpawner;
    
    [Header("Guns and Bullets")]
    public GameObject bullet;
    private int currentDamage = 10;

    [Header("Hardcore Gun")] 
    [SerializeField] private GameObject thunder;
    [SerializeField] private float hardcoreGunRange = -2f;
    [SerializeField] private LayerMask enemyLayer;

    public float fireRate = 0.5f;

    //Gun presets
    public GameObject GunPresetOne;
    public GameObject GunPresetTwo;
    private List<GameObject> GunPresetThree = new List<GameObject>();
    
    //Shot
    public bool isShooting = true;
    public GunState gunState;
    
    private void Start()
    {
        InitData();

        
        StartCoroutine(Fire());
    }

    private void InitData()
    {
        _gameManager = GameManager.Instance;
        _enemySpawner = _gameManager.GetComponent<EnemySpawner>();
        
        //Load presets
        GunPresetThree.Add(GunPresetOne);
        GunPresetThree.Add(GunPresetTwo);
    }

    private IEnumerator Fire()
    {
        while (isShooting)
        {
            switch (gunState)
            {
                case GunState.PresetOne:
                    TurnOnThor(false);
                    FirePresetOne();
                    break;
                case GunState.PresetTwo:
                    TurnOnThor(false);
                    FirePresetTwo();
                    break;
                case GunState.Hardcore:
                    FireHardcore();
                    break;
                case GunState.StopFiring:
                    StopShooting();
                    break;
                default:
                    throw new NotImplementedException();
            }
            
            yield return new WaitForSeconds(fireRate);
            yield return null;
        }
        yield return null;
    }

    private void StopShooting()
    {
        isShooting = false;
    }

    private void FirePresetOne()
    {
        Transform spawnAt = GunPresetOne.transform.GetChild(0);
        Vector3 spawnPos = spawnAt.position;
        GameObject projectile = Instantiate(bullet, spawnPos, Quaternion.identity);
        projectile.GetComponent<ProjectileDamage>().ProjectileDamageProperty = currentDamage;
    }

    private void FirePresetTwo()
    {
        Transform spawnAtOne = GunPresetTwo.transform.GetChild(0);
        Transform spawnAtTwo = GunPresetTwo.transform.GetChild(1);
        
        Vector3 spawnPosO = spawnAtOne.position;
        Vector3 spawnPosT = spawnAtTwo.position;
        
        GameObject projectileO = Instantiate(bullet, spawnPosO, Quaternion.identity);
        projectileO.GetComponent<ProjectileDamage>().ProjectileDamageProperty = currentDamage;
        
        GameObject projectileT = Instantiate(bullet, spawnPosT, Quaternion.identity);
        projectileT.GetComponent<ProjectileDamage>().ProjectileDamageProperty = currentDamage;
    }
    
    
    private void FireHardcore() 
    {
        Collider2D[] foundEnemies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 4f),
            new Vector2(8, 8), 0f, enemyLayer);


        List<GameObject> inField = CheckIfInRange(foundEnemies);
        
        
        float closest = 99f;
        GameObject closestObj = null;
        
        if (inField.Count > 0)
        {
            //Calculate distance and choose
            foreach (GameObject enemy in inField)
            {
                float currentlyClosest = Vector2.Distance(enemy.transform.position, transform.position);

                if (currentlyClosest < closest)
                {
                    closest = currentlyClosest;
                    closestObj = enemy.gameObject;
                }
            }
            
            //Fire
            TurnOnThor(true, closestObj);
        }
        else
        {
            //Stop Fire
            TurnOnThor(false);
        }
    }

    private void TurnOnThor(bool turnOn, GameObject enemyPos)
    {
        thunder.GetComponent<LightningBoltPrefabScript>().Source = GunPresetOne.transform.GetChild(0).gameObject;
        thunder.GetComponent<LightningBoltPrefabScript>().Destination = enemyPos;
        thunder.SetActive(turnOn);
        StartCoroutine(KillEnemy(enemyPos));
    }
    
    private void TurnOnThor(bool turnOn)
    {
        thunder.SetActive(turnOn);
    }

    private List<GameObject> CheckIfInRange(Collider2D[] enemiesTotal)
    {
        Collider2D[] enemies = enemiesTotal;
        List<GameObject> enemiesInField = new List<GameObject>();

        foreach (Collider2D enemy in enemies)
        {
            //Check Right

            float yPos = enemy.transform.position.y;

            //Formula for my Linear function y = 5x - 0.7
            // a = 5, b = 0.7
            float xRightEdge = ((yPos - (transform.position.y - 0.7f)) / 5) + transform.position.x;
            float xLeftEdge = ((yPos - (transform.position.y - 0.7f)) / -5) + transform.position.x;
            
            //Check left and right
            if (enemy.transform.position.x < xRightEdge && enemy.transform.position.x > xLeftEdge)
            {
                enemiesInField.Add(enemy.gameObject);
            }
        
        }

        return enemiesInField;
    }

    private IEnumerator KillEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(fireRate - 0.01f);
        //Destroy(enemy);
        enemy.GetComponent<EnemyDamageDealer>().EnemyDie();
        
        yield return null;
    }
    
    public enum GunState
    {
        PresetOne,
        PresetTwo,
        Hardcore,
        StopFiring
    }
    
}
