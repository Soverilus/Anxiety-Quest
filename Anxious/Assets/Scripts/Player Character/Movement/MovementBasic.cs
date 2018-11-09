using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasic : MonoBehaviour {
    CharacterStats myCS;
    JumpController myJC;
    Rigidbody2D myRB;
    float myInput;
    public float myMoveSpeed;
    float moveSpeed;
    public float maxMoveSpeed;
    float myAngle;
    void Start() {
        myCS = GetComponent<CharacterStats>();
        myRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        moveSpeed = myMoveSpeed * myRB.mass;
        myInput = Input.GetAxisRaw("Horizontal");
        MovementCalc(myInput);
        if (myCS.canMove) {
            MovementFunct();
        }
    }

    public float MovementCalc(float input) {
        Vector2 myMoveForce;
        myMoveForce = new Vector2(ReturnAngleDir(input, Vector3.left).x * myMoveSpeed, ReturnAngleDir(input, Vector3.left).y);
        return myMoveForce.x;
    }

    void MovementFunct() {
        if (myRB.velocity.x < maxMoveSpeed && myInput > 0f) {
            myRB.AddForce(ReturnAngleDir(myInput, Vector3.left) * moveSpeed);
        }
        if (myRB.velocity.x > -maxMoveSpeed && myInput < 0f) {
            myRB.AddForce(ReturnAngleDir(myInput, Vector3.left) * moveSpeed);
        }
        if (
            (myRB.velocity.x > 0f && myInput <= 0f) ||
            (myRB.velocity.x < 0f && myInput >= 0f) ||
            (myRB.velocity.x > maxMoveSpeed) ||
            (myRB.velocity.x < -maxMoveSpeed)
            ) {
            myRB.AddForce(new Vector2(myRB.velocity.x * -1.25f * moveSpeed, 0f));
        }
    }

    Vector2 ReturnAngleDir(float moveForce, Vector2 direction) {
        Vector2 returnDir;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit)
            returnDir = moveForce * hit.collider.gameObject.transform.TransformDirection(-direction);
        else returnDir = new Vector2(moveForce, 0);
        //ReturnAngleForce(hit);
        return returnDir;
    }

    void ReturnAngleForce(RaycastHit2D hit) {
        myAngle = Vector2.Angle(hit.normal, Vector2.up);
    }
    
}