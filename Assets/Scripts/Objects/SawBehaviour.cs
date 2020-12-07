using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloopy.Objects
{
    public class SawBehaviour : ObjectGeneral
    {
        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            Vector3 newPos = transform.position;
            newPos.y = Random.Range(8, 24);
            transform.position = newPos;
        }
    }
}