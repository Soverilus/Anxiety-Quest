using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public float intensity;
    public float maxX;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag != "Trap") {
            CharacterStats entityCS;
            Rigidbody2D targetRB;
            if (collision.gameObject.GetComponent<CharacterStats>()) {
                entityCS = collision.gameObject.GetComponent<CharacterStats>();
                entityCS.DamageMe(damage);
            }
            targetRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 bounceVector = new Vector2(targetRB.transform.position.x - collision.GetContact(0).point.x, targetRB.transform.position.y - collision.GetContact(0).point.y).normalized;
            bounceVector.x *= maxX;
            if (bounceVector.x == 0f) AntiClamp(bounceVector.x, -1f, 1f);
            if (bounceVector.x > 0f) Mathf.Clamp(bounceVector.x, 1f, maxX);
            if (bounceVector.x < 0f) Mathf.Clamp(bounceVector.x, -1f, -maxX);
            if (bounceVector.y != 0f) bounceVector.y /= intensity;
            targetRB.velocity = bounceVector * intensity;
        }
    }

    float AntiClamp(float myRandFloat, float minValue, float maxValue) {
        float calcFloat = myRandFloat;
        calcFloat += Random.Range(minValue, maxValue);
        if (calcFloat > -0.01f && calcFloat < 0.01f) {
            AntiClamp(myRandFloat, minValue, maxValue);
        }
        myRandFloat = calcFloat;
        return myRandFloat;
    }
}
