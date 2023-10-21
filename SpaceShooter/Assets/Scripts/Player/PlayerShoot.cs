using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.ThunderAndLightning;
using UnityEditor;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Guns and Bullets")]
    public GameObject bullet;
    private int currentDamage = 10;

    [Header("Hardcore Gun")] 
    [SerializeField] private GameObject thunder;
    [SerializeField] private float hardcoreGunRange = -2f;
    [SerializeField] private LayerMask enemyLayer;

    public float fireRate = 1f;

    //Gun presets
    public GameObject GunPresetOne;
    public GameObject GunPresetTwo;
    private List<GameObject> GunPresetThree = new List<GameObject>();
    
    //Shot
    public bool isShooting = true;
    
    private void Start()
    {
        InitData();
        
        StartCoroutine(Fire());
    }

    private void InitData()
    {
        //Load presets
        GunPresetThree.Add(GunPresetOne);
        GunPresetThree.Add(GunPresetTwo);
    }

    private IEnumerator Fire()
    {
        while (isShooting)
        {
            //FirePresetOne();

            FireHardcore();

            yield return new WaitForSeconds(fireRate);
            yield return null;
        }
        yield return null;
    }

    private void FirePresetOne()
    {
        
        Transform spawnAt = GunPresetOne.transform.GetChild(0);
        Vector3 spawnPos = spawnAt.position;
        GameObject projectile = Instantiate(bullet, spawnPos, Quaternion.identity);
        projectile.GetComponent<ProjectileDamage>().ProjectileDamageProperty = currentDamage;
    }
    
    
    private void FireHardcore()
    {
        //REMEMBER TO REMOVE LINE BELOW - IT WILL WORK FROM EnemySpawner
        fireRate = 0.1f;

        bool foundAnything = false;
        
        Collider2D[] foundEnemies = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + 4f),
            new Vector2(8, 8), 0f, enemyLayer);
        
        if(foundEnemies != null) foundAnything = true;
        
        
        List<GameObject> inField = CheckIfInRange(foundEnemies);
        

        if (foundAnything)
        {
            float closest = 99f;
            GameObject closestObj = null;
        
            //Calculate distance and choose
            foreach (GameObject enemy in inField) // zaraz zamień dwa na inRange
            {
                float currentlyClosest = Vector2.Distance(enemy.transform.position, transform.position);

                if (currentlyClosest < closest)
                {
                    closest = currentlyClosest;
                    closestObj = enemy.gameObject;
                }
            }
            
            TurnOnThor(true, closestObj); 
            
        }
        else
        {
            TurnOnThor(false);
        }
        
    }

    private void TurnOnThor(bool turnOn, GameObject enemyPos)
    {
        thunder.GetComponent<LightningBoltPrefabScript>().Source = GunPresetOne.transform.GetChild(0).gameObject;
        thunder.GetComponent<LightningBoltPrefabScript>().Destination = enemyPos;
        thunder.SetActive(turnOn);
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

    
}
