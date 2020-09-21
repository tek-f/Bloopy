using UnityEngine;
using System.Collections.Generic;

//The general spawn script that all spawn scripts will inherit from
public class SpawnGeneral : MonoBehaviour
{
    protected float spawnDelay;
    public float maxSpawnDelay;
    public float minSpawnDelay;
    protected float spawnTimeStamp;
    protected PlayerHandler player;
    public bool spawning;
    public void SpawnObject(GameObject prefab)
    {
        Instantiate(prefab);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
    }
}
