using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;

namespace Bloopy.Spawn
{
    public class SpawnSystemInitializer : MonoBehaviour
    {
        [SerializeField] PlayerHandler player;
        //[SerializeField] List<SpawnGeneral> spawnScripts = new List<SpawnGeneral>();
        [SerializeField] SpawnGeneral[] spawnScripts1;
        [SerializeField] int initialSpawnDelay;
        private void Awake()
        {
            spawnScripts1 = gameObject.GetComponents<SpawnGeneral>();
        }
        void Update()
        {
            if(player.distanceTravelled > initialSpawnDelay)
            {
                foreach (SpawnGeneral spawnScript in spawnScripts1)
                {
                    spawnScript.enabled = true;
                }
                this.enabled = false;
            }
        }
    }
}