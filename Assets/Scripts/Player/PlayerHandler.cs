using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.Spawn;
using Bloopy.GameManagement;

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
        float groundRateOfDecceleration = 0.9f;
        [SerializeField]
        public float boostSpeed = 2.0f;
        [Header("UI/Display")]
        public GameObject deathPanel, hudPanel;
        public Text distanceDisplay, speedDisplay, coinDisplay;
        public int coin;
        [Header("Launching")]
        public float launchSpeed;
        bool readyToLaunch, launched;
        [Header("Spawning")]
        public List<float> SpawnThresholds = new List<float>();
        [Header("Test Only")]
        public float jumpingTimeStamp = 0;
        public SpawnHazards spawnTester;
        #endregion
        #region Functions
        public void PrepareToLaunch()
        {
            readyToLaunch = true;
        }
        public void Launch()
        {
            GameManager.singleton.Launch();
            Debug.Log("Launched");
            speed = launchSpeed;
            playerRigidBody.AddForce(new Vector2(0, launchSpeed), ForceMode2D.Impulse);
            hudPanel.SetActive(true);
            //spawner.SetActive(true);
            readyToLaunch = false;
            launched = true;
        }
        void Death()
        {
            Time.timeScale = 0;
            deathPanel.SetActive(true);
        }
        public void ReduceSpeed(float _rateOfDeceleration)
        {
            speed = speed * _rateOfDeceleration;
        }
        /// <summary>
        /// Handles player behvaiour on collision with objects depending on the objects tag.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.transform.tag)
            {
                case "Ground":
                    if(speed > 10)
                    {
                        speed = 0.8f * speed;
                    }
                    else
                    {
                        speed -= 2;
                    }
                    break;
                //case "Boost":
                //    speed += boostSpeed;//increase the players speed by boost speed
                //    break;
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
                //case "Boost":
                //    speed += boostSpeed;//increase the players speed by boost speed
                //    break;
            }
        }
        private void Awake()
        {
            playerRigidBody = GetComponent<Rigidbody2D>();
            readyToLaunch = false;
            launched = false;
            //spawnTester = GameObject.FindWithTag("Spawner").GetComponent<SpawnHazards>();
        }
        private void Update()
        {
            #region Slam
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
            if (launched)
            {
                if (speed <= 0.9f)
                {
                    Death();
                }
                #endregion
                #region Update UI
                distanceTravelled += speed * Time.deltaTime;
                distanceDisplay.text = "Distance: " + (int)distanceTravelled;
                speedDisplay.text = "Speed: " + (int)speed;
                #endregion
            }
        }
    }
}