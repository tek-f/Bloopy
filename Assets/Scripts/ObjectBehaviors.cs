using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Player;
using Bloopy.GameManagement;

namespace Bloopy.Objects
{
    public class ObjectBehaviors : MonoBehaviour
    {
        Rigidbody2D playerRigidbody;
        float screenBuffer = 2.0f;

        public virtual void MoveObject()
        {
            if (NewGameManager.singleton.objectsMoving)
            {
                Vector3 _tempVector3 = transform.position;
                _tempVector3.y -= (playerRigidbody.velocity.y * Time.deltaTime);
                transform.position = _tempVector3;
            }
            if(transform.position.y < (-NewGameManager.singleton.player.vScreenRange - screenBuffer))
            {
                if (transform.CompareTag("Platform"))
                {
                    PlatformSpawner.singleton.platformInstance = null;
                }
                Destroy(gameObject);
            }
        }
        protected virtual void Start()
        {
            playerRigidbody = NewGameManager.singleton.player.GetComponent<Rigidbody2D>();
        }
        protected void Update()
        {
            MoveObject();
        }
    }
}