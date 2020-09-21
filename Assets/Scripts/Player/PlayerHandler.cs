using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Bloopy.spawn;

public class PlayerHandler : MonoBehaviour
{
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
    public float distanceTravelled;
    public GameObject deathPanel;
    public Text distanceDisplay, speedDisplay;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            speed -= speed * rateOfDecceleration;
        }
        if (collision.transform.tag == "Boost")
        {
            speed += boostSpeed;
        }
        if(collision.transform.tag == "Coin")
        {
            coin++;
        }
        if(collision.transform.tag == "Death")
        {
            Debug.Log("You are dead");
            Death();
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
    void SpawnChecker()
    {
        if(distanceTravelled > 100)
        {
            spawnTester.spawning = true;
        }
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
            if(readyToLaunch)
            {
                Launch();
            }
            else
            {
                playerRigidBody.velocity = new Vector2(0, slamSpeed);
            }
        }
        #endregion
        #region Spawning
        //Debug.Log(speed);
        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(distanceTravelled);
        }
        #endregion
        distanceTravelled += speed * Time.deltaTime;
        distanceDisplay.text = "Distance: " + distanceTravelled.ToString();
        speedDisplay.text = "Speed: " + speed.ToString();
        SpawnChecker();
    }
    #endregion
}
