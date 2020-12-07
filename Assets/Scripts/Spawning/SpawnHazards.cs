using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class SpawnHazards : SpawnGeneral
    {
        /// <summary>
        /// List of prefabs of hazzards
        /// </summary>
        [SerializeField] List<GameObject> Prefabs = new List<GameObject>();
        private void Update()
        {
            if (Time.time - spawnTimeStamp > spawnDelay)
            {
                SpawnObject(Prefabs[Random.Range(0, Prefabs.Count)]);
            }
        }
    }
}