using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ColliderSizeSetToSprite : MonoBehaviour {
    SpriteRenderer mySR;
    BoxCollider2D myBC;

	void Start () {
        mySR = GetComponent<SpriteRenderer>();
        myBC = GetComponent<BoxCollider2D>();
	}

    private void Update() {
        myBC.size = mySR.size;
    }
}
