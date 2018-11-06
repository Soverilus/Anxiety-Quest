using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public bool canJump;
    public bool canWallJump;
    public bool canSprint;
    public bool canFocus;
    public int anxietyLevel;

    public void DamageMe(int dmg) {
        anxietyLevel -= dmg;
        //display screenwide anxiety text that fades out creepily and stuff fuck off
    }
}
