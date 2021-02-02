using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bloopy.Player;
using Bloopy.Platform;

namespace Bloopy.GameManagement
{
    public class NewGameManager : MonoBehaviour
    {
        #region Singleton
        public static NewGameManager singleton;
        #endregion
        public BloopyBehaviors player;
        Camera playerCamera;
        public float height;
        bool gamePlaying = false;
        public bool GamePlaying { get { return gamePlaying; } }
        public Text heightDisplay;
        public GameObject endGamePanel;
        public bool objectsMoving = false;

        public void StartGame()
        {
            gamePlaying = true;
            player.Launch();
            PlatformSpawner.singleton.enabled = true;
            height = 0;
            //cameraTrackingBehaviors.enabled = true;
        }

        public void EndGame()
        {
            gamePlaying = false;
            endGamePanel.SetActive(true);
            Time.timeScale = 0.0f;
        }

        private void Awake()
        {
            #region Singleton
            if(singleton == null)
            {
                singleton = this;
            }
            else if(singleton != this)
            {
                Destroy(gameObject);
            }
            #endregion

            #region Reference SetUp
            playerCamera = Camera.main;
            #endregion

            Time.timeScale = 1.0f;
        }
        private void Update()
        {
            if(!gamePlaying && Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
            else if(gamePlaying)
            {
                if(player.transform.position.y >= playerCamera.transform.position.y)
                {
                    height += (player.PlayerRigidBody.velocity.y * Time.deltaTime);
                    heightDisplay.text = height.ToString();
                }
                if(Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (mousePos.y < Camera.main.transform.position.y)
                    {
                        PlatformSpawner.singleton.SpawnPlatform(mousePos);
                    }
                    //RaycastHit hit0;
                    //if(Physics.Raycast(Input.mousePosition, Vector3.forward, out hit0, Mathf.Infinity))
                    //{
                    //    if(hit0.transform.GetComponent<PlatformBehavior>())
                    //    {
                    //        hit0.transform.GetComponent<PlatformBehavior>().IncreasePlatformPower();
                    //    }
                    //    else
                    //    {
                    //        platformSpawner.SpawnPlatform(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    //    }
                    //}
                }
            }
        }
    }
}
//TODO: Implament score display after game session ends.
//TODO: Implament high score system. Also research posting scores/tracking scores online.