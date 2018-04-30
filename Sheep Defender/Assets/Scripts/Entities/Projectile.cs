using UnityEngine;

public class Projectile : MonoBehaviour, IDamage {
    public float damage;

    public float Damage() {
        return damage;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up*1.3f);
    }
}

