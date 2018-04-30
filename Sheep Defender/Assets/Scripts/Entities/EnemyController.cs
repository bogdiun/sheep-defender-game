using UnityEngine;

public class EnemyController : MonoBehaviour {
    private IShoot[] weapons;

    public float hitPoints;
    public ParticleSystem fxDestroyed;
    public ParticleSystem fxDamaged;

    private float damagedAt;

    private void Start() {
        weapons = GetComponents<IShoot>();
        damagedAt = hitPoints * 0.25f;
    }

    private void FixedUpdate() {
        foreach (IShoot loaded in weapons) {
            loaded.Shoot("Player");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<DestroyOnHit>()) {
            Instantiate(fxDestroyed, transform.position, Quaternion.identity);
            return;
        }
        
        Projectile projectile = other.GetComponent<Projectile>();
        if (projectile) {
            hitPoints = hitPoints - projectile.Damage();
            Destroy(other.gameObject);

            if (hitPoints <= 0) {
                Destroy(gameObject);
            } else if (hitPoints <= damagedAt) {
                Instantiate(fxDamaged, transform.position, transform.rotation, transform);
            }
        }
    }
}