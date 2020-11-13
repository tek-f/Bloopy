using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Spawn;

namespace Bloopy.Player
{
    public class PlayerHandler : MonoBehaviour
    {
        public float distanceTravelled { get; private set; }
        #region Variables
        [Header("Movement")]
        [SerializeField]
        Rigidbody2D playerRigidBody;
        [SerializeField]
        float slamSpeed = -10.0f;
        [SerializeField]
        float maxVerticalSpeed = -10.0f;
        [SerializeField]
        float verticalSpeed;
        [SerializeField]
        public float speed;
        [SerializeField]
        float acceleration = 2.0f;
        [SerializeField]
        float rateOfDecceleration = 0.1f;
        [SerializeField]
        public float boostSpeed = 2.0f;
        [Header("UI/Display")]
        public GameObject deathPanel;
        public Text distanceDisplay, speedDisplay, coinDisplay;
        public int coin;
        [Header("Launching")]
        public float launchSpeed;
        bool readyToLaunch;
        [Header("Spawning")]
        public GameObject spawner;
        public List<float> SpawnThresholds = new List<float>();
        [Header("Test Only")]
        SpriteRenderer playerSpriteRenderer;
        Color red = new Color(1, 0, 0, 1);
        Color blue = new Color(0, 0, 1, 1);
        public float jumpingTimeStamp = 0;
        public SpawnHazards spawnTester;
        [Header("Controls")]
        public Dictionary<string, KeyCode> KeyBindings = new Dictionary<string, KeyCode>();//any key input to be added for mobile support
        #endregion
        #region Functions
        /// <summary>
        /// Handles player behvaiour on collision with objects depending on the objects tag.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.transform.tag)
            {
                case "Ground":
                    speed -= speed * rateOfDecceleration;//decrease player speed by rate of decceleration
                    break;
                case "Boost":
                    speed += boostSpeed;//increase the players speed by boost speed
                    break;
                case "Death":
                    Death();//player dies
                    break;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.transform.tag)
            {
                case "Coin":
                    coin++;//increase players coin count by 1
                    coinDisplay.text = "Coins:" + coin.ToString();
                    Destroy(collision.gameObject);//destroy the coin
                    print(coin);
                    break;
                case "Boost":
                    speed += boostSpeed;//increase the players speed by boost speed
                    break;
            }
        }
        public void Launch()
        {
            Debug.Log("Launched");
            speed = launchSpeed;
            playerRigidBody.AddForce(new Vector2(0, launchSpeed), ForceMode2D.Impulse);
            spawner.SetActive(true);
            readyToLaunch = false;
        }
        void Death()
        {
            Time.timeScale = 0;
            deathPanel.SetActive(true);
        }
        private void Start()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            playerSpriteRenderer = GetComponent<SpriteRenderer>();
            spawner = GameObject.FindWithTag("Spawner");
            readyToLaunch = true;
            spawnTester = GameObject.FindWithTag("Spawner").GetComponent<SpawnHazards>();
            Time.timeScale = 1;
        }
        private void Update()
        {
            #region Movement
            if (Input.GetMouseButtonDown(0))
            {
                if (readyToLaunch)
                {
                    Launch();
                }
                else
                {
                    playerRigidBody.velocity = new Vector2(0, slamSpeed);
                }
            }
            #endregion
            #region Update UI
            distanceTravelled += speed * Time.deltaTime;
            distanceDisplay.text = "Distance: " + distanceTravelled.ToString();
            speedDisplay.text = "Speed: " + speed.ToString();
            #endregion
        }
        #endregion
    }
}