using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class SpawnBouncePad : SpawnGeneral
    {
        [SerializeField] GameObject bouncePadPrefab;
        private void Awake()
        {
            gameLayer = 1;
            minSpawnDelay = 10;
            maxSpawnDelay = 15;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        void Update()
        {
            if (Time.time - spawnTimeStamp > spawnDelay)
            {
                SpawnObject(bouncePadPrefab);
            }
        }
    }
}