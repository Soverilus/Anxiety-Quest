using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    Rigidbody2D myRB;
    Vector2 myInput;
    public float myMoveSpeed;
    float moveSpeed;
    public float maxMoveSpeed;
	void Start () {
        myRB = GetComponent<Rigidbody2D>();
	}

    void Update() {
        moveSpeed = myMoveSpeed * myRB.mass;
        MovementFunct();
    }

    void MovementFunct() {
        myInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (myRB.velocity.sqrMagnitude < maxMoveSpeed) {
            myRB.AddForce(ReturnAngleDir(HorzForce(myInput)) * moveSpeed);
        }
        if (myRB.velocity.sqrMagnitude > 0) {
            myRB.AddForce(myRB.velocity.normalized * -0.5f * moveSpeed);
        }
    }

    Vector2 HorzForce(Vector2 myVector) {
        Vector2 myNewVector = new Vector2(myVector.x , 0);
        return myNewVector;
    }

    Vector2 ReturnAngleDir(Vector2 moveForce) {
        Vector2 returnDir;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
        if (hit.collider.gameObject != null) {
            returnDir = moveForce.normalized * hit.collider.gameObject.transform.TransformDirection(Vector3.right);
        }
        else returnDir = moveForce.normalized;
        return returnDir;
    }
}
