using UnityEngine;
using Bloopy.Spawn;
using Bloopy.Player;
using Bloopy.GameManagement;

namespace Bloopy.Objects
{
    [RequireComponent(typeof(Collider2D))]
    public class ObjectGeneral : MonoBehaviour
    {
        Vector2 position;
        float speed;
        public PlayerHandler player;
        float objectMinXPos = -30f;
        private void Awake()
        {
            player = GameManager.singleton.player;
        }
        private void Update()
        {
            position = transform.position;
            position.x -= GameManager.singleton.player.speed * Time.deltaTime;
            transform.position = position;
            if(transform.position.x <= objectMinXPos)
            {
                Destroy(gameObject);
            }
        }
    }
}