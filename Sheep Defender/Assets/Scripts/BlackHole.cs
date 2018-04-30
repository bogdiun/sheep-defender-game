using UnityEngine;

public class BlackHole : MonoBehaviour {
    public GameObject fxDestroy;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Enemy") {
            Instantiate(fxDestroy, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);

        }
    }

    void FixedUpdate() {
        transform.position += -transform.up * 0.05f * Time.deltaTime;
    }
}

//DestroyOnEnter, DestroyOnHit, DestroyOnExit?
//vs the function in controller, shredder, blackhole, asteroids etc.