using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Guns and Bullets")]
    public GameObject bullet;

    public GameObject GunPresetOne;
    public GameObject GunPresetTwo;
    private List<GameObject> GunPresetThree = new List<GameObject>();


    private bool isShooting = true;
    
    private void Start()
    {
        GunPresetThree.Add(GunPresetOne);
        GunPresetThree.Add(GunPresetTwo);
        StartCoroutine(Fire());
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
        Instantiate(bullet, spawnPos, Quaternion.identity);
    }
    
}
