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
        float slamForce;
        [SerializeField]
        float maxSlamSpeed;
        [SerializeField]
        float verticalSpeed;
        [SerializeField]
        public float speed;
        [SerializeField]
        float acceleration;
        [SerializeField]
        float groundRateOfDecceleration;
        [SerializeField]
        public float bloopVerticalBoost;
        [Header("UI/Display")]
        public GameObject deathPanel, hudPanel;
        public Text distanceDisplay, speedDisplay, coinDisplay;
        public int bloopCollected;
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
        public void SetVerticalVelocity(float _newVerticalVelocity)
        {
            playerRigidBody.velocity = new Vector2(0, _newVerticalVelocity);
        }
        public void AlterVerticalVelocity(float _alteration)
        {
            playerRigidBody.AddForce(new Vector2(0, _alteration), ForceMode2D.Impulse);
        }
        public void ReduceSpeed(float _rateOfDeceleration)
        {
            speed = speed * _rateOfDeceleration;
        }
        public void CollectCoin()
        {
            bloopCollected++;
            coinDisplay.text = "Coins:" + bloopCollected.ToString();
            print(bloopCollected);
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
                    speed = 0.8f * speed;
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
                //case "Coin":
                //    bloopCollected++;//increase players coin count by 1
                //    coinDisplay.text = "Coins:" + bloopCollected.ToString();
                //    Destroy(collision.gameObject);//destroy the coin
                //    print(bloopCollected);
                //    break;
                case "Bloop":
                    AlterVerticalVelocity(bloopVerticalBoost);//increase the players vertical speed by bloopVerticalBoost
                    Destroy(collision.gameObject);
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
                else if(playerRigidBody.velocity.y > maxSlamSpeed)
                {
                    AlterVerticalVelocity(slamForce);
                }
                else
                {
                    Debug.Log("ur going too damn fast son!");
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