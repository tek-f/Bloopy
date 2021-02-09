using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.GameManagement;
using Bloopy.Platform;

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
        /// <summary>
        /// The velocity of the player's rigidbody2D. Used for bounce calculations.
        /// </summary>
        Vector3 currentVelocity;
        /// <summary>
        /// The modifier that the speed of Bloopy is multiplied by when bouncing. This needs to be above 1 to allow the player to gain height.
        /// </summary>
        public float bounceMultiplyer = 1.2f;

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
            if(transform.position.y >= vScreenRange * 0.5f)
            {
                //set bloopy position to middle of screen
                Vector3 _tempVectorA = transform.position;
                _tempVectorA.y = vScreenRange * 0.5f;
                transform.position = _tempVectorA;
                //tell game manager to move platforms and objects at rate of the inverse of bloopy velocity
                NewGameManager.singleton.objectsMoving = true;
            }
            else
            {
                NewGameManager.singleton.objectsMoving = false;
            }
        }
        void Bounce(Vector3 _collisionNormal)
        {
            //Store the current speed of the rigidbody.
            float speed = currentVelocity.magnitude;

            //Calculate and store the bounce direction
            Vector3 direction = Vector3.Reflect(currentVelocity.normalized, _collisionNormal);

            //Set rigidbody velocity according to direction and speed, with modifiers
            playerRigidbody.velocity = direction * (speed * bounceMultiplyer);

            Debug.Log("Boing!");
        }
        void Bounce(Vector3 _collisionNormal, int _platformPower)
        {
            //Store the current speed of the rigidbody.
            float speed = currentVelocity.magnitude;

            //Calculate and store the bounce direction
            Vector3 direction = Vector3.Reflect(currentVelocity.normalized, _collisionNormal);

            //Set rigidbody velocity according to direction and speed, with modifiers
            playerRigidbody.velocity = direction * (speed * (bounceMultiplyer + _platformPower));

            Debug.Log("Boing!");
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            //If collision if with platform
            if(collision.transform.CompareTag("Platform"))
            {
                Bounce(collision.contacts[0].normal, collision.collider.GetComponent<PlatformBehavior>().PlatformPower);
                //Set the platformInstance in PlatformSpawner to null.
                PlatformSpawner.singleton.platformInstance = null;
                //Destroy the platform game object.
                Destroy(collision.gameObject);
            }
            else
            {
                Bounce(collision.contacts[0].normal);
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
            currentVelocity = playerRigidbody.velocity;
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