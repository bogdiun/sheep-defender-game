using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehaviour : MonoBehaviour {
    // could it implement IFireable, should it be an IHoming
    private Rigidbody2D body;
    private Transform target;
    private float moveForce;

    //int:
    public float rotForce = 360f;
    public float duration = 3f;
    private float startTime;

    private void Awake() {
        enabled = false;
    }

    public void Init(float moveForce, string targetTag) {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        body = GetComponent<Rigidbody2D>();
        this.moveForce = moveForce;
        startTime = Time.timeSinceLevelLoad;
        enabled = true;
    }

    private void Update() {
        if (Time.timeSinceLevelLoad - startTime > duration) enabled = false;
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float rot = Vector3.Cross(direction, transform.up).z;

        body.angularVelocity = -rot * rotForce;
        body.velocity = transform.up * moveForce;

    }
}