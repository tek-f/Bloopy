using UnityEngine;

namespace Bloopy.Spawn
{
    public class SpawnSpike : SpawnGeneral
    {
        public GameObject spikePrefab;
        private void FixedUpdate()
        {
            if (Time.time - spawnTimeStamp >= spawnDelay)
            {
                SpawnObject(spikePrefab);
                spawnTimeStamp = Time.time;
                spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            }
        }
    }
}