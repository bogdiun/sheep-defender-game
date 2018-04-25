using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//some implementation of another sort of gun, physics based or raybased
public class FaceGun : MonoBehaviour, Gun {
    public float firingForce;
    Transform target;

    public void Fire(string layerName, Vector2 direction) {
    }

    public void Stop() {
    }
}
