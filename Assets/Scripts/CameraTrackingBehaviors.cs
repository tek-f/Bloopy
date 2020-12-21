using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;

namespace Bloopy.GameManagement
{
    public class CameraTrackingBehaviors : MonoBehaviour
    {
        BloopyBehaviors player;

        private void Start()
        {
            player = NewGameManager.singleton.player;
        }
        private void Update()
        {
            if(player.transform.position.y - 2 > transform.position.y)
            {
                Vector3 newPos = new Vector3(0, player.transform.position.y - 2, -10);
                transform.position = newPos;
            }
        }
    }
}