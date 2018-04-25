using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private Gun[] guns;
    public float hitPoints;
    public ParticleSystem DestroyedEffect;
    private float damaged;

    private void Start() {
        guns = GetComponents<Gun>();
        damaged = hitPoints * 0.5f;
    }

    private void Update() {
        foreach (Gun loaded in guns) {
            loaded.Fire("Hostile Projectiles", Vector2.down);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile) {
            hitPoints -= projectile.GetDamage();
            Destroy(other.gameObject);

            if (hitPoints <= 0) {
                Destroy(gameObject);

            } else if (hitPoints <= damaged) {
                Instantiate(DestroyedEffect, transform.position, Quaternion.identity, transform);
            }
        }
    }
}