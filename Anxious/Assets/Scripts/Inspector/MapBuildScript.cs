using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapBuildScript : MonoBehaviour {
    public Sprite myMapSprite;
    public GameObject[] mapObjects;
    Texture2D myMapSheet;
    bool flip = false;
    float flipYPos = -float.MinValue;
    Vector2 mirrorTopCorner;
    GameObject mirrorObj;
    GameObject flippedTiles;
    GameObject normalTiles;

    private void Awake() {
        myMapSheet = myMapSprite.texture;
    }

    public void BuildMap() {
        //Debug.Log("It Worked");
        TestBuildMap();
    }

    void TestBuildMap() {
        flippedTiles = new GameObject();
        normalTiles = new GameObject();
        GameObject currentTile = null;
        Color myColor;
        //left to right, top to bottom.

        for (int h = myMapSheet.height; h > 0; h--) {
            for (int w = 0; w < myMapSheet.width; w++) {
                myColor = myMapSheet.GetPixel(w, h);

                if (myColor == Color.blue) {
                    //Blue = Platform
                    TileInstantiation(currentTile, h, w, 0);
                }

                if (myColor == Color.red) {
                    //Red = Fake Platform
                    TileInstantiation(currentTile, h, w, 1);
                }

                if (myColor == Color.white) {
                    //Yellow = Special Platform
                    if (mirrorObj == null) {
                        mirrorObj = Instantiate(mapObjects[2], new Vector2(w * mapObjects[2].transform.lossyScale.x, h * mapObjects[2].transform.lossyScale.y), Quaternion.identity);
                        mirrorObj.transform.parent = flippedTiles.transform;
                        mirrorTopCorner = mirrorObj.transform.position;
                    }
                    Vector2 currentVector = new Vector2(w * mapObjects[2].transform.lossyScale.x, h * mapObjects[2].transform.lossyScale.y);
                    flip = true;
                    flipYPos = currentVector.y;
                    float currentX = currentVector.x;
                    float currentY = currentVector.y;
                    float centerX = (currentX - mirrorTopCorner.x);
                    float centerY = (currentY - mirrorTopCorner.y);
                    mirrorObj.transform.position = new Vector2(centerX/2, 0);
                    mirrorObj.transform.localScale = new Vector2(centerX, centerY);
                }

                if (myColor == Color.black) {
                    currentTile = Instantiate(mapObjects[2], new Vector2(w * mapObjects[2].transform.lossyScale.x, h * mapObjects[2].transform.lossyScale.y), Quaternion.identity);
                    currentTile.transform.parent = gameObject.transform;
                }

                if (myColor == Color.magenta) {
                    //Magenta = Trap                       
                    TileInstantiation(currentTile, h, w, 3);
                }

                if (myColor == Color.yellow) {
                    //Black = Enemy                        
                    TileInstantiation(currentTile, h, w, 4);
                }

                if (myColor == Color.green) {
                    //Green = Spawn
                    GameObject.FindGameObjectWithTag("PlayerHolder").transform.position = new Vector2(w * mapObjects[0].transform.lossyScale.x, h * mapObjects[0].transform.lossyScale.y);
                }
            }
        }
        Debug.LogWarning(gameObject.name + " built!");
    }

    void TileInstantiation(GameObject currentTile, int height, int width, int myTile) {
        currentTile = Instantiate(mapObjects[myTile], new Vector2(width * mapObjects[myTile].transform.lossyScale.x, height * mapObjects[myTile].transform.lossyScale.y), Quaternion.identity);
        currentTile.transform.parent = normalTiles.transform;
        if (currentTile.transform.position.y < flipYPos) {
            bool areThereMoreMirrorTiles = false;
            Color tempColor;
            for (int i = 0; i < myMapSheet.width; i++) {
                tempColor = myMapSheet.GetPixel(i, height);
                if (tempColor == Color.white) {
                    areThereMoreMirrorTiles = true;
                }
            }
            if (areThereMoreMirrorTiles) {
                flip = false;
            }
        }
        if (flip && currentTile.transform.position.y <= flipYPos) {
            currentTile.transform.Rotate(0, 0, 180);
            Vector2 oldPos = currentTile.transform.position;
            currentTile.transform.parent = flippedTiles.transform;
        }
    }
}