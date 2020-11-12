using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class Boost : SpawnGeneral
    {
        public GameObject boostTestPrefab;
        private void Start()
        {
            maxSpawnDelay = 10.0f;
            minSpawnDelay = 5.0f;
        }
        private void FixedUpdate()
        {
            //78 seconds, the time for a boost sprite to move accross the ground sprite at a speed of 1
            if (Time.time - spawnTimeStamp >= spawnDelay)
            {
                SpawnObject(boostTestPrefab);
            }
        }
    }
}