using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Objects
{
    public class SawBehaviour : ObjectGeneral
    {
        // Start is called before the first frame update
        protected void Start()
        {
            Vector3 newPos = transform.position;
            newPos.y = Random.Range(Mathf.Clamp(player.transform.position.y - 8, 1, player.transform.position.y), Mathf.Clamp(player.transform.position.y + 8, player.transform.position.y, 50));
            transform.position = newPos;
        }
    }
}