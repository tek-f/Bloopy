using UnityEngine;
using Bloopy.spawn;
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectGeneral : MonoBehaviour
{
    Vector2 position;
    float speed;
    PlayerHandler player;
    public SpawnTest spawner;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Object Catcher")
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
