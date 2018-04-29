using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    private IFireable[] guns;
    public float hitPoints;
    public ParticleSystem fxDestroyed;
    public ParticleSystem fxDamaged;
    private float damagedAt;

    private void Start() {
        guns = GetComponents<IFireable>();
        damagedAt = hitPoints * 0.25f;
    }

    private void FixedUpdate() {
        foreach (IFireable loaded in guns) {
            loaded.Fire("Player");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile) {
            hitPoints = hitPoints - projectile.Damage();
            Destroy(other.gameObject);

            if (hitPoints <= 0) {
                Instantiate(fxDestroyed, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } else if (hitPoints <= damagedAt) {
                Instantiate(fxDamaged, transform.position, Quaternion.identity, transform);
            }
        }
    }
}