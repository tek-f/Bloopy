using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
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
        SaveData saveData = new SaveData();
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
                Vector3 platformPos = Camera.main.ScreenToWorldPoint(platformPositionAction.ReadValue<Vector2>());
                if (platformPos.y < Camera.main.transform.position.y)
                {
                    PlatformSpawner.singleton.SpawnPlatform(platformPos);
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

            //Load previous high score.
            saveData = SaveSystem.singleton.LoadGame();

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
                if(player.transform.position.y >= playerCamera.transform.position.y)
                {
                    //Increase height by the players velocity over time.
                    height += (player.PlayerRigidBody.velocity.y * Time.deltaTime);
                    //Set heightDisplay to equal current height.
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