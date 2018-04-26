using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// once player is defeated make one of the enemy ships desert and become the player
public class PlayerController : MonoBehaviour {
    // private GunSystem gunSystem; //weapon
    private LevelManager levelManager;
    private IFireable[] guns;
    public float hitPoints;
    public float speed;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    void Start() {
        guns = GetComponents<IFireable>();
        SetMovementConstraints();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    void FixedUpdate() {
        Vector2 localPosition = transform.position;
        Debug.Log("enter:" + transform.position);
        //movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal > 0) {
            localPosition.x += (Vector3.right * speed * Time.deltaTime).x;

        } else if (horizontal < 0) {
            localPosition.x += (Vector3.left * speed * Time.deltaTime).x;
        }

        float vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0) {
            localPosition.y += (Vector3.up * speed * 0.8f * Time.deltaTime).y;

        } else if (vertical < 0) {
            localPosition.y += (Vector3.down * speed * 0.8f * Time.deltaTime).y;
        }

        float xClamp = Mathf.Clamp(localPosition.x, xMin, xMax);
        float yClamp = Mathf.Clamp(localPosition.y, yMin, yMax);
        transform.position = new Vector3(xClamp, yClamp, 0);
        Debug.Log("exit:" + transform.position);
    }

    private void Update() {
        // Fire a IFireable projectile    | implement conditions for more gunss
        // find a way to normalize, so if both variations get pressed it still counted as single press
        // and clicking vs holding
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            guns[0].Fire("Friendly Projectiles", Vector2.up);

        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            guns[0].Stop();

        } else if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1)) {
            guns[1].Fire("Friendly Projectiles", new Vector2(Random.value / 2f, 1f));
            guns[1].Fire("Friendly Projectiles", new Vector2(-Random.value / 2f, 1f));
        }
    }

    private void SetMovementConstraints() {
        float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));

        xMax = rightBoundary.x - 1f;
        xMin = leftBoundary.x + 1f;
        // do not hardcode..
        yMax = 4.2f;
        yMin = 0.2f;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        Debug.Log(projectile.name);

        if (projectile) {
            if (projectile.GetComponent<FaceGun>()) Debug.Log("-30 hit");

            hitPoints -= projectile.GetDamage();
            Destroy(other.gameObject);

            if (hitPoints <= 0) {
                // Destroy(gameObject);
                levelManager.LoadLevel("Lose");
            }
        }
    }
}
