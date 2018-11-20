using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FallingStoryDisplayer : MonoBehaviour {
    Camera mainCam;
    Text text;

    public float fadeInTime;
    public float stayDuration;
    public bool hasFadedIn;
    public bool hasFadedOut;


    void Start() {
        text = GetComponent<Text>();
        mainCam = Camera.main.GetComponent<Camera>();
        TextToNoAlpha();
    }

    void Update() {
        CSTT();
    }

    void TextToNoAlpha() {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    //"Cam Size To Text"
    void CSTT() {
        if (mainCam.orthographicSize >= fadeInTime + stayDuration && !hasFadedOut) {
            FadeTextToZeroAlpha(1/stayDuration, text);
        }

        else if (mainCam.orthographicSize >= fadeInTime && !hasFadedIn) {
            FadeTextToFullAlpha(1/stayDuration, text);
        }
    }

    void FadeTextToFullAlpha(float t, Text i) {
        if (i.color.a < 1.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
        }
        //to have this be a static function:
        //optional return bool
        //return color
        else hasFadedIn = true;
    }

    void FadeTextToZeroAlpha(float t, Text i) {
        if (i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
        }
        else hasFadedOut = true;
    }
}
