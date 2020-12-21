using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[AddComponentMenu("Player/Controller/Handler")]
public class PlayerHandlerTest : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    float runSpeed = 5.0f;
    float movement = 0;
    Vector2 playerPosition;
    [Header("Jumping")]
    public Rigidbody2D playerRigidBody;
    public bool touchingGround = true;
    public float jumpSpeed = 5.0f;
    float defultJumpSpeed = 5.0f;
    [Header("Super Jumping")]
    public float superJumpWindow = 1.0f;
    float defultSuperJumpWindow = 1.0f;
    float superJumpWindowReduction = 0.2f;
    float jumpSpeedBonus = 2.5f;
    public float landingTimeStamp = 0f;
    bool superJumpReady = false;
    bool bouncing = false;
    [Header("Test Only")]
    SpriteRenderer playerSpriteRenderer;
    Color red = new Color(1, 0, 0, 1);
    Color blue = new Color(0, 0, 1, 1);
    public float jumpingTimeStamp = 0;
    #endregion
    #region Functions
    private void SuperJumpCheck()
    {
        if (Time.time - landingTimeStamp > superJumpWindow)
        {
            playerRigidBody.gravityScale = 1;
            bouncing = false;
            superJumpReady = false;
            jumpSpeed = defultJumpSpeed;
            superJumpWindow = defultSuperJumpWindow;
            playerSpriteRenderer.color = blue;
        }
    }
    #endregion
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        #region Horizontal Movement
        playerPosition = transform.position;//Sets playerPosition
        if(!bouncing)
        {
            movement = Input.GetAxis("Horizontal");//sets movement to equal GetAxis horizontal value
        }
        else { movement = 0; }
        playerPosition.x += movement * runSpeed * Time.deltaTime;
        transform.position = playerPosition;//moves player according to movement
        #endregion
        #region Jumping
        if(touchingGround)
        {
            SuperJumpCheck();
        }
        if(Input.GetKeyDown(KeyCode.Space) && superJumpReady)//if super jump is ready, and if player is pressing space
        {
            bouncing = false;
            playerRigidBody.gravityScale = 1;
            jumpSpeed = defultJumpSpeed * (landingTimeStamp - jumpingTimeStamp + 1f);//jump speed is increased
            playerRigidBody.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);//Player super jumps
            touchingGround = false;//player isn't touch the ground
            superJumpReady = false;//super jump is not ready
            superJumpWindow -= superJumpWindowReduction;//time after landing that super jump is available is reduced by a set amount
        }
        else if(Input.GetKeyDown(KeyCode.Space) && touchingGround)
        {
            playerRigidBody.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);//player normal jumps
            touchingGround = false;//player isn't touch the ground
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            bouncing = false;
            playerRigidBody.gravityScale = 1;
        }
        #endregion
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            touchingGround = true;
            superJumpReady = true;
            bouncing = true;
            playerRigidBody.velocity = new Vector2(0, 0);
            landingTimeStamp = Time.time;
            playerSpriteRenderer.color = red;
            playerRigidBody.gravityScale = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Ground"))
        {
            jumpingTimeStamp = Time.time;
            touchingGround = false;
            superJumpReady = false;
            bouncing = false;
            playerSpriteRenderer.color = blue;
        }
    }
}
