using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
    [System.Serializable]
    public class Constraint { public Vector2 min, max; }
    public Constraint constraint;

    public float spawnDelay = 2f;
    private bool moveRight;

    public float speed;
    public float width;
    public float height;

    //temp
    bool respawn = true;

    void Start() {
        SetMovementConstraints();
        SpawnNextPosition();
        moveRight = (Random.value >= 0.5f);
    }

    void FixedUpdate() {
        if (transform.position.x >= constraint.max.x) moveRight = false;
        else if (transform.position.x <= constraint.min.x) moveRight = true;

        // movement
        if (moveRight) transform.Translate(Vector2.right * speed * Time.deltaTime);
        else transform.Translate(Vector2.left * speed * Time.deltaTime);

        // constrain alternative
        // float x = Mathf.Clamp(transform.position.x, constraint.min.x, constraint.max.x);
        // transform.position = new Vector3(x, transform.position.y);
    }

    private void Update() {
        if (FormationDestroyed()) {
            float random = Random.Range(constraint.min.x, constraint.max.x);
            transform.position = new Vector2(random, transform.position.y);
            respawn = true;

            foreach (Transform child in transform) {        // BossEnable 
                if (!child.gameObject.activeSelf) {
                    child.gameObject.SetActive(true);
                }
            }
            SpawnNextPosition();
        }
    }

    private void SpawnNextPosition() {
        Transform next = NextFreePosition();

        if (next) next.GetComponent<SpawnPosition>().Spawn();
        if (respawn) {    //temp
            if (NextFreePosition()) Invoke("SpawnNextPosition", spawnDelay);   //or respawnRate
            else respawn = false;
        }
    }

    private Transform NextFreePosition() {
        //or just GetFreePos and then select one in a desired order
        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf && child.childCount == 0) {
                return child;
            }
        }
        return null;
    }

    private bool FormationDestroyed() {
        //aka GetFreePos == Pos.count, this however looks only until a child is found
        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf && child.childCount > 0) {
                return false;
            }
        }
        return true;
    }

    private void SetMovementConstraints() {
        Camera camera = Camera.main;
        float cameraDistance = transform.position.z - camera.transform.position.z;  //unnecessary in ortho
        constraint.min = Vector2.one + (Vector2) camera.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        constraint.max = -Vector2.one + (Vector2) camera.ViewportToWorldPoint(new Vector3(1, 1, cameraDistance));
        constraint.min.y = constraint.max.y * 0.4f; //min max y is set differently to player/enemy.

        // formation padding, should constraint to leftmost and rightmost enemy on it's spawn/destroy
        constraint.max.x -= width / 2;
        constraint.min.x += width / 2;
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector2(width, height));
        Gizmos.DrawWireCube(transform.position, new Vector2(width - 0.25f, height - 0.25f));
    }
}
