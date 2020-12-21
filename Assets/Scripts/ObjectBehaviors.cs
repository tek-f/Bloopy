using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;
using Bloopy.GameManagement;

namespace Bloopy.Objects
{
    public class ObjectBehaviors : MonoBehaviour
    {
        private void Update()
        {
            if(NewGameManager.singleton.player.transform.position.y > transform.position.y + 7)
            {
                Destroy(gameObject);
            }
        }
    }
}