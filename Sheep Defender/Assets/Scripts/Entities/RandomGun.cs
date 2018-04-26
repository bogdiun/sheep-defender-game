using UnityEngine;

public class RandomGun : ProjectileGun {   //implement Gun, +Input?
    public override void Fire(string layerName, Vector2 direction) {
        base.direction = direction;
        base.layerName = layerName;

        float probability = Time.deltaTime * firingRate;
        if (Random.value < probability) Spawn();
    }

    public override void Stop() {
        //stop doing something
    }
}