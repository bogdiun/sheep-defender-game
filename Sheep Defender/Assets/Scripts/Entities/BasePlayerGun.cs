using UnityEngine;

public class BasePlayerGun : ProjectileGun {   //implement Gun, +Input?
    public override void Fire(string layerName, Vector2 direction) {
        base.direction = direction;
        base.layerName = layerName;
        InvokeRepeating("Spawn", 0.01f, firingRate);
    }

    public override void Stop() {
        CancelInvoke();
    }
}