using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
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
        public TMP_Text heightDisplay;
        public GameObject endGamePanel;
        public bool objectsMoving = false;

        public PlayerInput gameplayActions;
        public InputAction interactAction;
        public InputAction platformPositionAction;

        private void OnInteractPerformedStartGame(InputAction.CallbackContext _context)
        {
            if (!gamePlaying)
            {
                StartGame();
            }
        }

        private void OnInteractPerformedGamePlaying(InputAction.CallbackContext _context)
        {
            if(gamePlaying)
            {
                Vector3 platformPos = Camera.main.ScreenToWorldPoint(platformPositionAction.ReadValue<Vector2>());
                if (platformPos.y < Camera.main.transform.position.y)
                {
                    PlatformSpawner.singleton.SpawnPlatform(platformPos);
                }
            }
        }

        public void StartGame()
        {
            gamePlaying = true;
            PlatformSpawner.singleton.enabled = true;
            height = 0;

            platformPositionAction.Enable();

            interactAction.performed -= OnInteractPerformedStartGame;
            interactAction.performed += OnInteractPerformedGamePlaying;

            player.Launch();
        }

        public void EndGame()
        {
            gamePlaying = false;
            endGamePanel.SetActive(true);
            Time.timeScale = 0.0f;

            interactAction.Disable();
            platformPositionAction.Disable();
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

            #region Input SetUp
            interactAction = gameplayActions.actions.FindAction("Interact");
            interactAction.Enable();
            interactAction.performed += OnInteractPerformedStartGame;

            platformPositionAction = gameplayActions.actions.FindAction("PlatformPosition");
            #endregion
        }
        private void Update()
        {
            if(gamePlaying)
            {
                if(player.transform.position.y >= playerCamera.transform.position.y)
                {
                    height += (player.PlayerRigidBody.velocity.y * Time.deltaTime);
                    heightDisplay.text = ((int)height).ToString();
                }
                #region Raycasting For Platform Power Increase TBD
                //    //RaycastHit hit0;
                //    //if(Physics.Raycast(Input.mousePosition, Vector3.forward, out hit0, Mathf.Infinity))
                //    //{
                //    //    if(hit0.transform.GetComponent<PlatformBehavior>())
                //    //    {
                //    //        hit0.transform.GetComponent<PlatformBehavior>().IncreasePlatformPower();
                //    //    }
                //    //    else
                //    //    {
                //    //        platformSpawner.SpawnPlatform(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //    //    }
                //    //}
                #endregion
            }
        }
    }
}
//TODO: Implament score display after game session ends.
//TODO: Implament high score system. Also research posting scores/tracking scores online.