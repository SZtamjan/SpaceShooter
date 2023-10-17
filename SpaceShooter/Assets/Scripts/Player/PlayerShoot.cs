using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Guns and Bullets")]
    public GameObject bullet;
    private int currentDamage = 10;
    
    //Gun presets
    public GameObject GunPresetOne;
    public GameObject GunPresetTwo;
    private List<GameObject> GunPresetThree = new List<GameObject>();
    
    //Shot
    private bool isShooting = true;
    
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
            FirePresetOne();

            yield return new WaitForSeconds(0.5f);
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
    
}
