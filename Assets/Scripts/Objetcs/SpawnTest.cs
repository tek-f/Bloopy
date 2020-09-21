using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : SpawnGeneral
{
    public GameObject boostTestPrefab;
    private void FixedUpdate()
    {
        //78 seconds
        if(Time.time - spawnTimeStamp >= spawnDelay)
        {
            SpawnObject(boostTestPrefab);
            spawnTimeStamp = Time.time;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
    private void Start()
    {
        maxSpawnDelay = 50.0f;
        minSpawnDelay = 5.0f;
    }
}
