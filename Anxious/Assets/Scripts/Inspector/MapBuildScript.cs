using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapBuildScript : MonoBehaviour {
    public Sprite myMapSprite;
    public GameObject[] mapObjects;
    Texture2D myMapSheet;

    private void Awake() {
        myMapSheet = myMapSprite.texture;
    }

    public void BuildMap() {
        Debug.Log("It Worked");
    }

    void TestBuildMap() {
        Color myColor;
        for (int h = 0; h < myMapSheet.width; h++) {
            for (int w = 0; w < myMapSheet.height; w++) {
                myColor = myMapSheet.GetPixel(w, h);

                if (myColor == Color.blue) {
                    //Blue = Platform
                    Instantiate(mapObjects[0], new Vector2(w, h), Quaternion.identity);
                }

                if (myColor == Color.red) {
                    //Red = Fake Platform
                    Instantiate(mapObjects[1], new Vector2(w, h), Quaternion.identity);
                }

                if (myColor == Color.yellow) {
                    //Yellow = Special Platform
                    Instantiate(mapObjects[2], new Vector2(w, h), Quaternion.identity);
                }

                if (myColor == Color.magenta) {
                    //Magenta = Trap
                    Instantiate(mapObjects[3], new Vector2(w, h), Quaternion.identity);
                }

                if (myColor == Color.black) {
                    //Black = Enemy
                    Instantiate(mapObjects[4], new Vector2(w, h), Quaternion.identity);
                }

                if (myColor == Color.green) {
                    //Green = Spawn
                    GameObject.FindGameObjectWithTag("PlayerHolder").transform.position = new Vector2(w,h);
                }

                if (myColor == Color.white) {
                    //White = ???
                    //Instantiate(mapObjects[6], new Vector2(w, h), Quaternion.identity);
                }
            }
        }
    }
}