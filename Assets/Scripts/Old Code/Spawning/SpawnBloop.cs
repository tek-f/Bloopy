using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Objects;
using Bloopy.GameManagement;

namespace Bloopy.Spawn
{
    public class SpawnBloop : SpawnGeneral
    {
        [SerializeField] GameObject bloopPrefab;
        float bloopPlayerPositionVerticalSpawnOffset = 5.0f, bloopPlayerPositionHorizontalSpawnOffset = 3.0f;

        public override void SpawnObject(GameObject prefab)
        {
            Transform newBloop = Instantiate(prefab).transform;
            newBloop.GetComponent<ObjectGeneral>().player = player;
            Vector3 newPos = newBloop.position;
            newPos.y = Random.Range(player.transform.position.y, player.transform.position.y + bloopPlayerPositionVerticalSpawnOffset);
            newPos.x = Random.Range(player.transform.position.x - bloopPlayerPositionHorizontalSpawnOffset, player.transform.position.x + bloopPlayerPositionHorizontalSpawnOffset);
            newBloop.position = newPos;
            spawnTimeStamp = Time.time;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);

        }
        private void Awake()
        {
            minSpawnDelay = 1f;
            maxSpawnDelay = 4f;
            spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        }

        private void Update()
        {
            if(Time.time - spawnTimeStamp >= spawnDelay)
            {
                SpawnObject(bloopPrefab);
            }
        }
    }
}