using UnityEngine;

public class BasePlayerGun : ProjectileGun {   //implement Gun, +Input?
    public AudioClip pewpewClip;

    public override void Fire(string layerName, Vector2 direction) {
        base.direction = direction;
        base.layerName = layerName;
        InvokeRepeating("Shoot", 0.01f, firingRate);
    }

    public override void Stop() {
        CancelInvoke();
    }

    public void Shoot() {
        base.Spawn();
        AudioSource.PlayClipAtPoint(pewpewClip, transform.position, 0.3f);
    }
}