using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
    // private GameObject[] enemyPrefabs;
    public float spawnDelay = 2f;

    public float speed;
    public float width = 10f;
    public float height = 5f;

    private float xMax;
    private float xMin;
    private bool moveRight = true;

    //temp
    bool doRespawn = true;

    void Start() {
        SetMovementConstraints();
        SpawnNextPosition();
    }

    void FixedUpdate() {
        // movement, TODO:Constraint based on the current live formation
        if (transform.position.x >= xMax) {
            moveRight = false;
        } else if (transform.position.x <= xMin) {
            moveRight = true;
        }

        if (moveRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (FormationDestroyed()) {
            Debug.Log("Formation Destroyed");
            // BossEnable
            foreach (Transform child in transform) {
                if (!child.gameObject.activeSelf) {
                    child.gameObject.SetActive(true);
                }
            }
            doRespawn = true;
            SpawnNextPosition();
        }

        // float xClamp = Mathf.Clamp(transform.position.x, xMin, xMax);
        // transform.position = new Vector3(xClamp, transform.position.y);
    }

    private void SpawnNextPosition() {
        Transform next = NextFreePosition();

        if (next) next.GetComponent<SpawnPosition>().Spawn();
        if (doRespawn) {    //temp
            if (NextFreePosition()) Invoke("SpawnNextPosition", spawnDelay);   //or respawnRate
            else doRespawn = false;
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
        float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));

        xMax = rightBoundary.x - width / 2;
        xMin = leftBoundary.x + width / 2;
        // TODO: constraint to leftmost and rightmost enemy on spawn and on destroy
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector2(width, height));
        Gizmos.DrawWireCube(transform.position, new Vector2(width - 0.25f, height - 0.25f));
    }
}
