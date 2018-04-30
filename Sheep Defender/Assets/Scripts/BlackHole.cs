using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Enemy") 
        Destroy(other.gameObject);
    }

    void FixedUpdate() {
        transform.position += -transform.up * 0.05f * Time.deltaTime;
    }
}
