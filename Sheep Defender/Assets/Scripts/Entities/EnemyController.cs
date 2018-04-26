using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private IFireable[] guns;
    public float hitPoints;
    public ParticleSystem DestroyedEffect;
    public ParticleSystem DamagedEffect;
    public bool targetPlayer;
    private Transform target;
    private Rigidbody2D body;
    private float damagedAt;

    private void Start() {
        guns = GetComponents<IFireable>();
        damagedAt = hitPoints * 0.5f;
        body = GetComponent<Rigidbody2D>();
        if (targetPlayer) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate() {
        foreach (IFireable loaded in guns) {
            if (targetPlayer) {
                Vector2 direction = (Vector2) target.position - body.position;
                direction.Normalize();
                loaded.Fire("Hostile Projectiles", direction);
            } else loaded.Fire("Hostile Projectiles", Vector2.down);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile) {
            hitPoints -= projectile.GetDamage();
            Destroy(other.gameObject);

            if (hitPoints <= 0) {
                Instantiate(DestroyedEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);

            } else if (hitPoints <= damagedAt) {
                Instantiate(DamagedEffect, transform.position, Quaternion.identity, transform);
            }
        }
    }
}