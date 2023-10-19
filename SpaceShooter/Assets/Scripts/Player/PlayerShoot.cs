using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Guns and Bullets")]
    public GameObject bullet;
    private int currentDamage = 10;
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

    public GameObject dbg;
    
    private void FireHardcore()
    {
        Collider2D[] dwa = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + 0.2f, transform.position.y + hardcoreGunRange), new Vector2(transform.position.x - 0.2f, transform.position.y + hardcoreGunRange),enemyLayer);
        
        foreach (Collider2D col in dwa)
        {
            Debug.Log("wykryto " + col.gameObject.name);
            Instantiate(dbg, col.transform);
        }
    }
}
