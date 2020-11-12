using UnityEngine;
using System.Collections.Generic;
using Bloopy.Player;

//The general spawn script that all spawn scripts will inherit from
namespace Bloopy.Spawn
{
    public class SpawnGeneral : MonoBehaviour
    {
        /// <summary>
        /// The time between spawns. Used in Update to check if SpawnObject() should run. Is set in SpawnObject to a random number between minSpawnDelay and maxSpawnDelay
        /// </summary>
        protected float spawnDelay;
        /// <summary>
        /// Used to set spawnDelay. The maximum value spawnDelay can be set to.
        /// </summary>
        public float maxSpawnDelay;
        /// <summary>
        /// Used to set spawnDelay. The minimum value spawnDelay can be set to.
        /// </summary>
        public float minSpawnDelay;
        /// <summary>
        /// Time stamp of the previous spawn. Used in Update to check if SpawnObject() should run.
        /// </summary>
        protected float spawnTimeStamp;
        /// <summary>
        /// Base spawnning function to be used by Spawner scripts to spawn in objects.
        /// </summary>
        /// <param name="prefab">Prefab to be instanciated/spawned</param>
        public virtual void SpawnObject(GameObject prefab)
        {
            Instantiate(prefab);
            spawnTimeStamp = Time.time;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        private void Start()
        {
            spawnTimeStamp = Time.time;
        }
    }
}