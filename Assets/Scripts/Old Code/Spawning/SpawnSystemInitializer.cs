using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;

namespace Bloopy.Spawn
{
    public class SpawnSystemInitializer : MonoBehaviour
    {
        /// <summary>
        /// Reference to the player. Set in the inspector.
        /// </summary>
        [SerializeField] PlayerHandler player;
        /// <summary>
        /// Contains all instances of scripts that inherit from SpawnGeneral(). Is set on Awake().
        /// </summary>
        [SerializeField] SpawnGeneral[] spawnScripts;
        /// <summary>
        /// The time from the game starting to the spawn scripts being activated.
        /// </summary>
        [SerializeField] int initialSpawnDelay;
        private void Awake()
        {
            spawnScripts = gameObject.GetComponents<SpawnGeneral>();
        }
        void Update()
        {
            if(player.distanceTravelled > initialSpawnDelay)
            {
                foreach (SpawnGeneral spawnScript in spawnScripts)
                {
                    spawnScript.enabled = true;
                }
                this.enabled = false;
            }
        }
    }
}