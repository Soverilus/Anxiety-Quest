using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {
    Rigidbody2D myRB;
    Renderer myRend;
    private void OnCollisionExit2D(Collision2D collision) {
        myRend = GetComponent<Renderer>();
        myRB = gameObject.AddComponent<Rigidbody2D>();
        myRB.mass = 100000f;
        myRB.angularDrag = 0f;
        myRB.drag = 0f;
        myRB.gravityScale = 1.05f;
        myRB.angularVelocity = (transform.position.x + collision.transform.position.x) *10f;
    }

    private void Update() {
        if (myRend != null && !myRend.isVisible) {
            Destroy(gameObject);
        }
    }
}
