using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    public bool grounded;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
    private Rigidbody2D rb;

    void Start() {
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update() {
        JumpFunct();

        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);


        //if we are grounded...
        if (grounded) {
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }
    }

    void JumpFunct() {

        //if you press down the jump button...
        if (Input.GetAxis("Jump") > 0) {
            //and you are on the ground...
            if (grounded) {
                //jump!
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
            }
        }

        //if you keep holding down the jump button...
        if (Input.GetAxis("Jump") > 0 && !stoppedJumping) {
            //and your counter hasn't reached zero...
            if (jumpTimeCounter > 0) {
                //keep jumping!
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }


        //if you stop holding down the jump button...
        if (Input.GetAxis("Jump") < 0) {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }


    /*
     Rigidbody2D myRB;
     float myInput;
     public float myMoveSpeed;
     float moveSpeed;
     public float maxMoveSpeed;
     bool isGrounded = false;
     [SerializeField]
     float jumpForce;
     float timer;
     bool hasJumped = false;

     void Start() {
         myRB = GetComponent<Rigidbody2D>();
         timer = 0f;
     }

     void FixedUpdate() {
         //Debug.Log(isGrounded);
         moveSpeed = myMoveSpeed * myRB.mass;
         MovementFunct();
     }

     void MovementFunct() {
         myInput = Input.GetAxisRaw("Horizontal");
         if (myRB.velocity.sqrMagnitude < maxMoveSpeed) {
             myRB.AddForce(ReturnAngleDir(myInput, Vector3.left) * moveSpeed);
         }
         if (myRB.velocity.sqrMagnitude > 0) {
             myRB.AddForce(myRB.velocity.normalized * -0.5f * moveSpeed);
         }

         put an object under the player that checks for ground
         if ground is jumpable, allow jumping
         
         if (isGrounded) {
             if (Input.GetAxis("Jump") > 0f) {
                 if (!hasJumped) {
                     myRB.AddForce(ReturnAngleDir(myRB.mass * jumpForce, Vector2.down));
                     hasJumped = true;
                 }
             }
             timer += Time.fixedDeltaTime;
             if (myRB.velocity.y <= 0f && timer > 0.5f) {
                 isGrounded = false;
                 hasJumped = false;
                 timer = 0f;
             }
         }
         if (Input.GetAxis("Jump") <= 0f) {
             myRB.AddForce(new Vector2(0f, myRB.mass * 0.5f * Physics.gravity.y));
         }
}

    Vector2 ReturnAngleDir(float moveForce, Vector2 direction) {
        Vector2 returnDir;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit)
            returnDir = moveForce * hit.collider.gameObject.transform.TransformDirection(-direction);
        else returnDir = new Vector2(moveForce, 0);
        return returnDir;
    }

private void OnCollisionStay2D(Collision2D collision) {
    if (!isGrounded) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.25f);
        if (hit) {
            isGrounded = true;
        }
    }*/
}