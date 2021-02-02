using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Objects;
using Bloopy.GameManagement;

namespace Bloopy.Platform
{
    public class PlatformBehavior : ObjectBehaviors
    {
        int platformPower = 0;
        //public int platformsIndex;
        public float spawnTimestamp { get; private set; }
        public void IncreasePlatformPower()
        {
            if(platformPower < 2)
            {
                platformPower++;
            }
        }
    }
}