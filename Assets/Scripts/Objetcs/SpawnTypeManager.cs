using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTypeManager : SpawnGeneral
{
    //public GameObject TestspikePrefab;
    public List<GameObject> Prefabs = new List<GameObject>();//List of prefabs of game objects of the type that the script manages (i.e. power ups, enemies/obstacles)
    private void Start()
    {
        maxSpawnDelay = 10.0f;
        minSpawnDelay = 5.0f;
    }
    private void FixedUpdate()
    {
        if(spawning)
        {
            if (Time.time - spawnTimeStamp > spawnDelay)
            {
                //SpawnObject(TestspikePrefab);
                Debug.Log("Spike spawned");
                SpawnObject(Prefabs[Random.Range(0, Prefabs.Count)]);
                spawnTimeStamp = Time.time;
                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
    }
}
