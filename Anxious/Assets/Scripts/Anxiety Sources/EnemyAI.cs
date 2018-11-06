using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Trap))]
public class EnemyAI : MonoBehaviour {
    enum MyAIState { Wander, Chase }
    MyAIState myState;
    Rigidbody2D myRB;
    GameObject myTarget;
    public float chaseSpeed;
    float walkSpeed;
    float randomInput;

    //Target is the player
    private void Start() {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        switch (myState) {
            case MyAIState.Wander:
                Wander();
                //continuously look for player - perhaps patrol
                break;
            case MyAIState.Chase:
                ChaseTarget();
                break;
            default:
                //fuck
                break;
        }
    }
    void ChaseTarget() {
        //chase the player
        //unless gap, in which case stop
    }
    void Wander() {
        randomInput = Random.Range(-1f, 1f);
        if (/* not close to an edge ==*/true) {
            if (myRB.velocity.x <= walkSpeed && myRB.velocity.x >= -walkSpeed) {
                myRB.AddForce(new Vector2(randomInput * walkSpeed, 0f));
            }
            if (myRB.velocity.x > 0f || myRB.velocity.x < 0f) {
                myRB.AddForce(new Vector2(myRB.velocity.x * -0.5f, 0f));
            }
        }
    }
}