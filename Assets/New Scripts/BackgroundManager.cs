using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public float speed;
    public float leftPosX;
    public float rightPosX;
    public float initialPosX; 

    void Start()
    {
        backgroundPrefab.transform.position = new Vector3(initialPosX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        backgroundupdate();
        checkbackground();
    }

    private void checkbackground()
    {
        if (backgroundPrefab.transform.position.x <= leftPosX)
        {
            backgroundPrefab.transform.position = new Vector3(rightPosX, transform.position.y, transform.position.z);
        }
    }

    private void backgroundupdate()
    {
        var movement = Time.deltaTime * speed;
        backgroundPrefab.transform.position = new Vector3(backgroundPrefab.transform.position.x - movement, transform.position.y, transform.position.z);

    }
}

