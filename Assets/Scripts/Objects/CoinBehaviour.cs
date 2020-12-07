using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Objects
{
    public class CoinBehaviour : ObjectGeneral
    {
        protected override void Awake()
        {
            //base.Awake();
            Vector3 newPos = transform.position;
            newPos.y = Random.Range(7, 25);
            transform.position = newPos;
        }
    }
}