using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    public float intensity;
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
            targetRB.velocity = (new Vector2(targetRB.transform.position.x - collision.GetContact(0).point.x, targetRB.transform.position.y - collision.GetContact(0).point.y).normalized * intensity);
        }

    }
}
