using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;

namespace Bloopy.Objects
{
    public class BouncePadBehavior : ObjectGeneral
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("Player"))
            {

            }
        }
    }
}