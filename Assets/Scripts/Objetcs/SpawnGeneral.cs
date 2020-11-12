using UnityEngine;
using System.Collections.Generic;
using Bloopy.Player;

//The general spawn script that all spawn scripts will inherit from
namespace Bloopy.Spawn
{
    public class SpawnGeneral : MonoBehaviour
    {
        protected float spawnDelay;
        public float maxSpawnDelay;
        public float minSpawnDelay;
        protected float spawnTimeStamp;
        public bool spawning;
        public void SpawnObject(GameObject prefab)
        {
            Instantiate(prefab);
            spawnTimeStamp = Time.time;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
    }
}