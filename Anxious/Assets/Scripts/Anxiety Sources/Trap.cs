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
            Vector2 bounceVector = new Vector2(targetRB.transform.position.x - transform.position.x, targetRB.transform.position.y - transform.position.y).normalized;
            bounceVector.x = AntiClamp(bounceVector.x, -maxX, maxX);
            targetRB.velocity = bounceVector * intensity;
        }
    }

    float AntiClamp(float myFloat, float minValue, float maxValue) {
        float midPoint = (minValue + maxValue) / 2f;
        int rand = -1;
        if ((myFloat > minValue) && (myFloat < maxValue)) {
            if (myFloat == midPoint) rand = Random.Range(0, 2);
            if (rand == 1) {
                myFloat += float.MinValue;
            }
            else if (rand == 0){
                myFloat -= float.MinValue;
            }
            if (myFloat > midPoint) myFloat = maxValue;
            if (myFloat < midPoint) myFloat = minValue;
        }
            return myFloat;
    }
}
