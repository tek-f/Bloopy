using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class Boost : SpawnGeneral
    {
        /// <summary>
        /// Prefab of the boost object to be spawned in SpawnObject().
        /// </summary>
        [SerializeField] GameObject boostTestPrefab;
        private void Update()
        {
            if (Time.time - spawnTimeStamp >= spawnDelay)
            {
                SpawnObject(boostTestPrefab);
            }
        }
    }
}