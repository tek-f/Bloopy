using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Bloopy.Player;
using Bloopy.Platform;
using Bloopy.UI;
using Bloopy.Saving;

namespace Bloopy.GameManagement
{
    public class NewGameManager : MonoBehaviour
    {
        #region Singleton
        public static NewGameManager singleton;
        #endregion

        #region Reference Variables
        [Header("Reference Variables")]
        /// <summary>
        /// Reference to the player's behavior script.
        /// </summary>
        public BloopyBehaviors player;
        /// <summary>
        /// Reference to the player's camera.
        /// </summary>
        Camera playerCamera;
        /// <summary>
        /// Tracks if the play session has started.
        /// </summary>
        bool gamePlaying = false;
        /// <summary>
        /// Access modfier for gamePlay bool. Get only.
        /// </summary>
        public bool GamePlaying { get { return gamePlaying; } }
        /// <summary>
        /// Reference to Text Mesh Pro element that displays the player's height during play on HUD.
        /// </summary>
        public TMP_Text heightDisplay;
        /// <summary>
        /// Reference to UI canvas panel that is displayed at the end of a play session.
        /// </summary>
        public GameObject endGamePanel;
        /// <summary>
        /// Reference to endGameDisplay script on endGamePanel. Handles display of final score and high score on endGamePanel.
        /// </summary>
        EndGameDisplay endGameDisplay;
        #endregion

        #region Gameplay Variables
        [Header("Gameplay Variables")]
        /// <summary>
        /// The height that the player has reached during play session (i.e. player's score).
        /// </summary>
        public float height;
        /// <summary>
        /// Tracks if objects are moving around the player. Is used to simulate player movement, when player is above the centre of the scree.
        /// </summary>
        public bool objectsMoving = false;
        /// <summary>
        /// SaveData file for this player.
        /// </summary>
        SaveData saveData;
        /// <summary>
        /// Publicily accessible reference to the highScore in saveData.
        /// </summary>
        public int highScore
        {
            get
            {
                return saveData.highScore;
            }
        }
        PlatformBehavior targetedPlatform = null;
        #endregion

        #region Input Variables
        [Header("Input Variables")]
        public PlayerInput gameplayActions;
        public InputAction interactAction;
        public InputAction platformPositionAction;
        #endregion

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
                if (targetedPlatform == null)
                {
                    Vector3 platformPos = playerCamera.ScreenToWorldPoint(platformPositionAction.ReadValue<Vector2>());
                    if (platformPos.y < player.vScreenRange * 0.5)
                    {
                        PlatformSpawner.singleton.SpawnPlatform(platformPos);
                    }
                    return;
                }
                else
                {
                    Debug.Log("HIT!!!");
                    targetedPlatform.IncreasePlatformPower();
                }
            }
        }

        public void StartGame()
        {
            //Set gamePlaying to true.
            gamePlaying = true;

            //Enable platform spawning.
            PlatformSpawner.singleton.enabled = true;

            //Reset score tracking.
            height = 0;

            //Enable platformPositionAction.
            platformPositionAction.Enable();

            //Remove start game input action method.
            interactAction.performed -= OnInteractPerformedStartGame;
            //Add game play input action method.
            interactAction.performed += OnInteractPerformedGamePlaying;

            //Launch player.
            player.Launch();
        }

        public void EndGame()
        {
            //Set gamePlaying to false.
            gamePlaying = false;

            //Destroy Bloopy game object to stop player falling.
            Destroy(player.gameObject);

            //If height is a new high score
            if(height > saveData.highScore)
            {
                //Set new high score.
                saveData.SetHighScore((int)height);
                //Save new high score.
                SaveSystem.singleton.SaveGame(saveData);
            }

            //Hide HUD height display from view.
            heightDisplay.gameObject.SetActive(false);

            //Display endGamePanel.
            endGamePanel.SetActive(true);

            //Disable gameplay input actions.
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

            saveData = new SaveData();

            //If save file exists
            if(File.Exists(Application.persistentDataPath + "/saveData.tsf"))
            {
                //Load save data
                saveData = SaveSystem.singleton.LoadGame();
            }
            else//If no save file exists
            {
                //Create new save file
                SaveSystem.singleton.SaveGame(saveData);
            }

            //Set timeScale to 1.
            Time.timeScale = 1.0f;

            #region Input SetUp
            //Find relevant gameplayActions
            interactAction = gameplayActions.actions.FindAction("Interact");
            platformPositionAction = gameplayActions.actions.FindAction("PlatformPosition");

            //Enable interactAction to allow for game to be started
            interactAction.Enable();

            //Add start game input action method.
            interactAction.performed += OnInteractPerformedStartGame;
            #endregion
        }
        private void Update()
        {
            //If game is being played.
            if(gamePlaying)
            {
                //If the player is above the centre of the screen
                if(player.transform.position.y >= player.vScreenRange * 0.5)
                {
                    //Increase height by the players velocity over time.
                    height += (player.PlayerRigidBody.velocity.y * Time.deltaTime);
                    //Set heightDisplay to equal current height.
                    heightDisplay.text = ((int)height).ToString();
                }
            }

        }
        private void FixedUpdate()
        {
            Vector3 mousePos = platformPositionAction.ReadValue<Vector2>();
            mousePos.z = 10;
            Vector3 screenPos = playerCamera.ScreenToWorldPoint(mousePos);
            RaycastHit2D hit = Physics2D.Raycast(screenPos, Vector2.zero);
            if(hit)
            {
                if (hit.collider.CompareTag("Platform"))
                {
                    targetedPlatform = hit.collider.GetComponent<PlatformBehavior>();
                }
            }
            else
            {
                targetedPlatform = null;
            }
        }
    }
}