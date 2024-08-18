using System;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject bg;
    public float rotateSpeed = 1f;
    private Vector3 rotationSpeed = new Vector3(0,0,0);
    
    private void Start()
    {
        rotationSpeed.z = -rotateSpeed;
    }

    void Update()
    {
        bg.transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
