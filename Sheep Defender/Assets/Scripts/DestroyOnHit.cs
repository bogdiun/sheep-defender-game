using UnityEngine;

public class DestroyOnHit : MonoBehaviour, IDamage {
    public GameObject fxDestroyed;
    public float hitPoints;
    public float damage;

    public float Damage() {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        IDamage hit = other.GetComponent<IDamage>();
        if (hit != null) hitPoints -= hit.Damage();

        Destroy(other.gameObject);
        if (hitPoints <= 0f) {
            Instantiate(fxDestroyed, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
    }
}

