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
        /// Access modfier for playerRigidbody Rigidbody2D. Get only.
        /// </summary>
        public Rigidbody2D PlayerRigidBody
        {
            get
            {
                return playerRigidbody;
            }
        }
        /// <summary>
        /// Force player is launched with on start of game.
        /// </summary>
        float launchForce = 25.0f;
        /// <summary>
        /// Reference to the camera used by the player.
        /// </summary>
        Camera playerCamera;
        /// <summary>
        /// Horizontal screen range, used for side looping. Equal to half the width of the screen in world space units. Calcualted in Awake().
        /// </summary>
        public float hScreenRange;
        /// <summary>
        /// Vertical screen range, used to detect when player is outside the bounds of the screen to trigger end game. Equal to half the height of the screen in world space units. Calcualted in Awake().
        /// </summary>
        public float vScreenRange;
        #region OLD CODE
        /// <summary>
        /// The velocity of the player's rigidbody2D. Used for bounce calculations.
        /// </summary>
        Vector3 currentVelocity;
        #endregion

        /// <summary>
        /// Launches player using rigidbody.AddForce(). Used in NewGameManager.StartGame().
        /// </summary>
        public void Launch()
        {
            playerRigidbody.AddForce(new Vector2(0, launchForce), ForceMode2D.Impulse);
        }

        /// <summary>
        /// Used to maintain Bloopy's position and track speed to move platforms and other objects down when Bloopy is at or above the centre of the screen.
        /// </summary>
        void Positioning()
        {
            //If Bloopy's position is above the centre of the screen
            if(transform.position.y >= playerCamera.transform.position.y)
            {
                //set bloopy position to middle of screen
                Vector3 _tempVectorA = transform.position;
                _tempVectorA.y = playerCamera.transform.position.y;
                transform.position = _tempVectorA;
                //tell game manager to move platforms and objects at rate of the inverse of bloopy velocity
                NewGameManager.singleton.objectsMoving = true;
            }
            else
            {
                NewGameManager.singleton.objectsMoving = false;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //If collision if with platform
            if(collision.transform.CompareTag("Platform"))
            {
                //Set the platformInstance in PlatformSpawner to null.
                PlatformSpawner.singleton.platformInstance = null;
                //Destroy the platform game object.
                Destroy(collision.gameObject);
            }
        }
        private void Awake()
        {
            //Set reference variables.
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerCamera = Camera.main;

            //Calculate the vertical range of the screen using playerCamera size.
            vScreenRange = playerCamera.orthographicSize;
            //Calculate the horizontal range of the screen using playerCamera size times the aspect ratio of the screen.
            hScreenRange = playerCamera.aspect * vScreenRange;
        }
        private void Update()
        {
            //If a game session has been started
            if (NewGameManager.singleton.GamePlaying)
            {
                Positioning();
                //If player is to the right of the screen
                if (transform.position.x > hScreenRange)
                {
                    //Move player to just outside the left of the screen
                    Vector3 tempPos = transform.position;
                    tempPos.x = -hScreenRange;
                    transform.position = tempPos;
                }
                //If player is the left of the screen
                if (transform.position.x < -hScreenRange)
                {
                    //Move player to just outside the right of the screen
                    Vector3 tempPos = transform.position;
                    tempPos.x = hScreenRange;
                    transform.position = tempPos;
                }
                //If player is below the screen range (plus buffer of 0.5f)
                if (transform.position.y - playerCamera.transform.position.y < -vScreenRange - 0.5f)
                {
                    //End game
                    NewGameManager.singleton.EndGame();
                }
            }
        }
    }
}