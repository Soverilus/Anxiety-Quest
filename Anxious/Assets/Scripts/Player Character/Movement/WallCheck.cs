using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour {
    JumpController myJC;
    MovementBasic myMB;

    private void Start() {
        myJC = GetComponentInParent<JumpController>();
        myMB = GetComponentInParent<MovementBasic>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        float dirMult;
        if (collision.transform.position.x < transform.position.x) {
            dirMult = 1f;
        }
        else {
            dirMult = -1f;
        }
        myJC.WallChecker(dirMult);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        myJC.wallRiding = false;
    }
}
