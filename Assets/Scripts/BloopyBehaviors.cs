using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.GameManagement;

namespace Bloopy.Player
{
    public class BloopyBehaviors : MonoBehaviour
    {
        /// <summary>
        /// Reference to player's Rigidbody2D.
        /// </summary>
        Rigidbody2D playerRigidbody;
        /// <summary>
        /// Force player is launched with on start of game.
        /// </summary>
        float launchForce = 25.0f;
        /// <summary>
        /// Horizontal screen range, used for side looping.
        /// </summary>
        public float hScreenRange;
        /// <summary>
        /// Vertical screen range, used to detect when player is outside the bounds of the screen to trigger end game.
        /// </summary>
        public float vScreenRange;

        /*TODO: See if hScreenRange and vScreenRange can be replaced with using the bounds of the camera. This will ensure differing screen sizes will not affect gameplay.
         *Alternate Options: use a trigger
         */

        /// <summary>
        /// Launches player using rigidbody.AddForce(). Used in NewGameManager.StartGame().
        /// </summary>
        public void Launch()
        {
            playerRigidbody.AddForce(new Vector2(0, launchForce), ForceMode2D.Impulse);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.CompareTag("Platform"))
            {
                //TODO: Change Bloopy bounce physics to be done manually, instead of using physicsmaterial2D and other Unity components.

                Transform collidedPlatform = collision.transform;

                //get the z rotation of the platform
                //get the vertical velocity

                PlatformSpawner.singleton.platformInstance = null;
                Destroy(collision.gameObject);
            }
        }
        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            //If a game session has been started
            if (NewGameManager.singleton.GamePlaying)
            {
                //If player is to the right of the screen
                if (transform.position.x > hScreenRange)
                {
                    //Move player to just outside the le
                    Vector3 tempPos = transform.position;
                    tempPos.x = -hScreenRange + 0.01f;
                    transform.position = tempPos;
                }
                if (transform.position.x < -hScreenRange)
                {
                    Vector3 tempPos = transform.position;
                    tempPos.x = hScreenRange - 0.01f;
                    transform.position = tempPos;
                }
                if (transform.position.y < Camera.main.transform.position.y - 5.5f)
                {
                    NewGameManager.singleton.EndGame();
                }
            }
        }
    }
}