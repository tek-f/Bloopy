using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Spawn
{
    public class CoinSpawner : SpawnGeneral
    {
        /// <summary>
        /// Prefab of the coin to be spawned in SpawnObject().
        /// </summary>
        [SerializeField] GameObject coinPrefab;
        /// <summary>
        /// The maximum value that a spawned coins position.y will be set to when spawned in SpawnObject(). Is set to 25 to keep it within the range of the screen. 
        /// </summary>
        [SerializeField] float spawnYPosRange = 25f;
        /// <summary>
        /// Override of base SpawnObject() from SpawnGeneral(). Uses base.SpawnObject() but also sets the position.y of the spawned object within the range of the prefabs position.y value and spawnYPosRange.
        /// </summary>
        /// <param name="prefab">Prefab of the coin to be spawned in.</param>
        public override void SpawnObject(GameObject prefab)
        {
            base.SpawnObject(prefab);
            Vector3 position = prefab.transform.position;
            position.y = Random.Range(7, 25);
            prefab.transform.position = position;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        private void Update()
        {
            if (Time.time - spawnTimeStamp >= spawnDelay)
            {
                SpawnObject(coinPrefab);
            }
        }
    }
}