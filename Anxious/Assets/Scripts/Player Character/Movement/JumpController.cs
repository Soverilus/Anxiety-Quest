using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {
    CharacterStats myCS;
    MovementBasic myMB;
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float wallJumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public float wallJumpTime;
    public float wallJumpTimeCounter;
    bool hasMovedThisFrame;
    public bool isHoldingJump;
    float myHorzMovement;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    public bool grounded;
    public bool wallRiding;
    float wallDirection;
    public LayerMask whatIsGround;
    public bool stoppedJumping;
    public bool stoppedWallJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody2D rb;

    void Start() {
        myCS = GetComponent<CharacterStats>();
        myMB = GetComponent<MovementBasic>();
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (myCS.canJump) {
            //hasMovedThisFrame = false;
            //myHorzMovement = Input.GetAxisRaw("Horizontal");
            JumpFunct();
            GroundChecker();
        }
        WallRidingChanger();
    }

    void GroundChecker() {
        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //if we are grounded...
        if (grounded && stoppedJumping) {
            //the jumpcounter is whatever we set jumptime to in the editor.
            if (!isHoldingJump) {
                jumpTimeCounter = 0;
            }
        }
    }
    void WallRidingChanger() {
        if (wallRiding) {
            if (rb.velocity.y <= 0f) {
                rb.velocity = new Vector2(rb.velocity.x, Physics.gravity.y * 0.75f * Time.deltaTime);
            }
        }
    }

    public void WallChecker(float dirMult) {
        if (!grounded) {
            wallRiding = true;
            wallDirection = dirMult * wallJumpForce * 1.25f;
            if (!isHoldingJump) {
                wallJumpTimeCounter = 0f;
            }
        }
    }

    void JumpFunct() {
        //if you press down the jump button...
        if (Input.GetAxisRaw("Jump") > 0) {
            if (!isHoldingJump) {
                isHoldingJump = true;
                //and you are on the ground...
                if (stoppedJumping) {
                    if (grounded && !wallRiding) {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        stoppedJumping = false;
                    }
                    if (myCS.canWallJump && stoppedWallJumping && wallRiding && !grounded) {
                        stoppedWallJumping = false;
                        rb.velocity = new Vector2(wallDirection, wallJumpForce);
                    }
                }
            }
        }
        //if you keep holding down the jump button...
        if (isHoldingJump) {
            if (!stoppedJumping) {
                //and your counter hasn't reached zero...
                if (jumpTimeCounter < jumpTime) {
                    jumpTimeCounter += Time.deltaTime;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
            }
            if (!stoppedWallJumping) {
                if (wallJumpTimeCounter < wallJumpTime) {
                    wallJumpTimeCounter += Time.deltaTime;
                    rb.velocity = new Vector2(wallDirection, wallJumpForce);
                }
            }
        }
        //if you stop holding down the jump button...
        if (Input.GetAxisRaw("Jump") == 0) {
            isHoldingJump = false;
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the Groundchecker function.
            jumpTimeCounter = jumpTime;
            stoppedWallJumping = true;
            stoppedJumping = true;
        }
    }
}