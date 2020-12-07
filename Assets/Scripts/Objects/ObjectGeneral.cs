using UnityEngine;
using Bloopy.Spawn;
using Bloopy.Player;

namespace Bloopy.Objects
{
    [RequireComponent(typeof(Collider))]
    public class ObjectGeneral : MonoBehaviour
    {
        Vector2 position;
        float speed;
        public PlayerHandler player;
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.tag == "Object Catcher")
            {
                Destroy(gameObject);
            }
        }
        protected virtual void Awake()
        {
            //player = GameObject.FindWithTag("Player").GetComponent<PlayerHandler>();
        }
        private void Update()
        {
            position = transform.position;
            position.x -= player.speed * Time.deltaTime;
            transform.position = position;
        }
    }
}