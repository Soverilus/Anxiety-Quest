﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject myTargetObj;
    Vector3 myTarget;
    private void Update() {
        myTarget = myTargetObj.transform.position;
        myTarget = UseMyZAxis(myTarget);
        transform.position = Vector3.Lerp(transform.position, myTarget, 0.25f);
    }

    private Vector3 UseMyZAxis(Vector3 target) {
        Vector3 myNewVector = new Vector3(target.x, target.y, transform.position.z);
        return myNewVector;
    }
}
