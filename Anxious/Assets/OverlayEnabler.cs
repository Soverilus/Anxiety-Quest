using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayEnabler : MonoBehaviour {
    public GameObject[] overlays;

    private void Start() {
        overlays[Random.Range(0, 3)].SetActive(true);
    }
}
