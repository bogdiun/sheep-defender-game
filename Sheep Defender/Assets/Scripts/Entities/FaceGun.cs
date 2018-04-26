using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//some implementation of another sort of gun, physics based or raybased
public class FaceGun : MonoBehaviour, IFireable {
    public float firingForce;
    private Rigidbody2D body;
    private Vector2 direction;


    private void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        float rot = Vector3.Cross(direction, transform.up).z;   //if has target as player I just send a different direction
        float rotForce = 180f;
        body.angularVelocity = -rot * rotForce;
        body.velocity = transform.up * firingForce;
    }

    public void Fire(string layerName, Vector2 direction) {
        this.direction = direction;
    }

    public void Stop() {
    }
}
