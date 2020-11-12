﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class SpawnHazards : SpawnGeneral
    {
        /// <summary>
        /// List of prefabs of hazzards
        /// </summary>
        public List<GameObject> Prefabs = new List<GameObject>();
        /// <summary>
        /// Modifier for minSpawnDelay and maxSpawnDelay. Spawn delay is multiplied by modifier to reduce the time between spawns
        /// </summary>
        [SerializeField] float spawnDelayModifier = 0.9f;
        /// <summary>
        /// The time until the spawn rate is increased
        /// </summary>
        [SerializeField] float timeToSpawnIncrease = 0.9f;
        private void Start()
        {
            maxSpawnDelay = 10.0f;
            minSpawnDelay = 5.0f;
        }
        private void FixedUpdate()
        {
            if (Time.time - spawnTimeStamp > spawnDelay)
            {
                SpawnObject(Prefabs[Random.Range(0, Prefabs.Count)]);
            }
        }
    }
}