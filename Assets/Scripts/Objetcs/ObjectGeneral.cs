using UnityEngine;
using Bloopy.Spawn;
using Bloopy.Player;

namespace Bloopy.Objects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ObjectGeneral : MonoBehaviour
    {
        Vector2 position;
        float speed;
        PlayerHandler player;
        public SpawnTest spawner;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Object Catcher")
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerHandler>();
            spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnTest>();
        }
        private void Update()
        {
            position = transform.position;
            position.x -= player.speed * Time.deltaTime;
            transform.position = position;
        }
    }
}