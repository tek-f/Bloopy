using UnityEngine;
using System.Collections.Generic;
using Bloopy.Player;
using Bloopy.Objects;

//The general spawn script that all spawn scripts will inherit from
namespace Bloopy.Spawn
{
    public class SpawnGeneral : MonoBehaviour
    {
        public PlayerHandler player;

        public int gameLayer;
        /// <summary>
        /// The time between spawns. Used in Update to check if SpawnObject() should run. Is set in SpawnObject to a random number between minSpawnDelay and maxSpawnDelay
        /// </summary>
        [SerializeField] protected float spawnDelay;
        /// <summary>
        /// Used to set spawnDelay. The maximum value spawnDelay can be set to.
        /// </summary>
        [SerializeField] protected float maxSpawnDelay;
        /// <summary>
        /// Used to set spawnDelay. The minimum value spawnDelay can be set to.
        /// </summary>
        [SerializeField] protected float minSpawnDelay;
        /// <summary>
        /// The time until the spawn rate is altered.
        /// </summary>
        [SerializeField] float spawnDelayModifier = 0.9f;
        /// <summary>
        /// The time until AlterSpawnDelay() is called, which alters the time until the next spawn.
        /// </summary>
        [SerializeField] float timeToSpawnDelayAlter;
        /// <summary>
        /// Time stamp of the previous spawn. Used in Update to check if SpawnObject() should run.
        /// </summary>
        protected float spawnTimeStamp;
        /// <summary>
        /// Time stamp of the previous alter of the spawn spawn delay. Used in Update to check if AlterSpawnDelay() should run.
        /// </summary>
        protected float spawnDelayAlterationTimeStamp;
        /// <summary>
        /// Base spawnning function to be used by Spawner scripts to spawn in objects.
        /// </summary>
        /// <param name="prefab">Prefab to be instanciated/spawned</param>
        public virtual void SpawnObject(GameObject prefab)
        {
            GameObject newObject = Instantiate(prefab);
            spawnTimeStamp = Time.time;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }
        /// <summary>
        /// Multiplies maxSpawnDelay and minSpawnDelay by spawnDelayModifier to alter the time between spawns. Sets spawnDelayAlterationTimeStamp to Time.time.
        /// </summary>
        protected void AlterSpawnDelay()
        {
            maxSpawnDelay *= spawnDelayModifier;
            minSpawnDelay *= spawnDelayModifier;
            spawnDelayAlterationTimeStamp = Time.time;
        }
    }
}