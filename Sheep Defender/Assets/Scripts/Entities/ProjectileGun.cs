using UnityEngine;

public abstract class ProjectileGun : MonoBehaviour, Gun {
    public Rigidbody2D projectilePrefab;
    public float firingForce;
    public float firingRate;

    protected Vector2 direction;
    protected string layerName;

    public abstract void Fire(string layerName, Vector2 direction);
    public abstract void Stop();
    public void Spawn() {
        Vector3 spawnPoint = transform.position + new Vector3(direction.x, direction.y, 0f);
        Rigidbody2D projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity) as Rigidbody2D;
        projectile.velocity = direction * firingForce;
        projectile.gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    //can probably avoid having getters though
    //three levels of dependencies does not look tooo good as well, but better than duplicate code and no extensibility?
}