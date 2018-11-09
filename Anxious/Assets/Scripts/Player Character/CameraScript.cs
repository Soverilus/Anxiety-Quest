using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    Camera thisCam;
    public GameObject myTargetObj;
    Vector3 myTarget;
    public bool fallScale;

    private void Start() {
        thisCam = GetComponent<Camera>();
    }

    private void FixedUpdate() {
        myTarget = myTargetObj.transform.position;
        FallScaleCam();
        transform.position = Vector3.Lerp(transform.position, UseMyZAxis(myTarget), 0.25f);
    }

    void FallScaleCam() {
        if (fallScale) {
            if (myTarget.y <= transform.position.y - thisCam.orthographicSize / 2) {
                thisCam.orthographicSize = (transform.position.y - myTarget.y) * 2f;
            }
        }
    }

    private Vector3 UseMyZAxis(Vector3 target) {
        Vector3 myNewVector = new Vector3(target.x, target.y, transform.position.z);
        return myNewVector;
    }
}
