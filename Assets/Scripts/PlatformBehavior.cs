using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Objects;
using Bloopy.GameManagement;

namespace Bloopy.Platform
{
    public class PlatformBehavior : MonoBehaviour
    {
        int platformPower = 0;
        //public int platformsIndex;
        public float spawnTimestamp { get; private set; }
        public void IncreasePlatformPower()
        {
            if(platformPower < 2)
            {
                platformPower++;
            }
        }
        private void Start()
        {
            spawnTimestamp = Time.time;
        }
        private void Update()
        {
            if (NewGameManager.singleton.player.transform.position.y > transform.position.y + 7)
            {
                PlatformSpawner.singleton.platformInstance = null;
                Destroy(gameObject);
            }
        }
    }
}