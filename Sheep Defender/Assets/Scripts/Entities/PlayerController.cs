using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// once player is defeated make one of the enemy ships desert and become the player
public class PlayerController : MonoBehaviour {
    // private GunSystem gunSystem; //weapon
    private Gun[] guns;
    public float hitPoints;
    public float speed;
    private float xMin;
    private float xMax;

    void Start() {
        guns = GetComponents<Gun>();
        SetGameBounds();
    }

    void Update() {
        //movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal > 0) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        
        } else if (horizontal < 0) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        float xClamp = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(xClamp, transform.position.y, 0);

        // Fire a Gun projectile    | implement conditions for more guns
        if (Input.GetKeyDown(KeyCode.Space)) {
            guns[0].Fire("Friendly Projectiles", Vector2.up);
        
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            guns[0].Stop();
        
        } else if (Input.GetKey(KeyCode.LeftShift)) {
            guns[1].Fire("Friendly Projectiles", new Vector2(Random.value / 2f, 1f));
            guns[1].Fire("Friendly Projectiles", new Vector2(-Random.value / 2f, 1f));
        }
    }

    public void SetGameBounds() {
        float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));

        xMax = rightBoundary.x - 1f;
        xMin = leftBoundary.x + 1f;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile) {
            hitPoints -= projectile.GetDamage();
            Destroy(other.gameObject);
           
            if (hitPoints <= 0) {
                Destroy(gameObject);
                Debug.Log("Game Lost;");
            }
        }
    }
}
