using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;

namespace Bloopy.Objects
{
    public class BloopBehavior : MonoBehaviour
    {
        float verticalBoost = 1;
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.transform.tag == "Player")
            {
                collision.GetComponent<PlayerHandler>().speed += 10;
                collision.GetComponent<PlayerHandler>().CollectCoin();
                Destroy(gameObject);
            }
        }
    }
}