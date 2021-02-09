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
        public int PlatformPower
        {
            get
            {
                return platformPower;
            }
        }

        public void IncreasePlatformPower()
        {
            if(platformPower < 2)
            {
                platformPower++;
            }
            Debug.Log(platformPower);
        }
    }
}