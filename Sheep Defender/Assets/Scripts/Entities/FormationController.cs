using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
    public GameObject[] enemyPrefabs;
    public float spawnDelay;
    //grids etc.

    public float width = 10f;
    public float height = 5f;

    public float speed;
    bool moveRight = true;
    private float xMax;
    private float xMin;

    void Start() {
        SpawnFormation();
        SetMovementConstraints();
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
            SpawnNextPosition();
        }

        // float xClamp = Mathf.Clamp(transform.position.x, xMin, xMax);
        // transform.position = new Vector3(xClamp, transform.position.y);
    }

    private bool FormationDestroyed() {
        foreach (Transform child in transform) {
            if (child.childCount > 0) return false;
        }
        return true;
    }

    //spawns in order as the children are in Hierarchy
    private void SpawnFormation() {
        foreach (Transform child in transform) {
            if (child.GetComponent<Position>().active) {
                if (child.tag == "Fast Enemy") {    //compare with each prefab
                    Instantiate(enemyPrefabs[2], child.position, Quaternion.identity, child);
                } else {
                    Instantiate(enemyPrefabs[1], child.position, Quaternion.identity, child);
                }
            }
        }
    }

    private Transform NextFreePosition() {
        foreach (Transform child in transform) {
            if (child.childCount == 0) return child;
        }
        return null;
    }

    private void SpawnNextPosition() {
        GameObject selPrefab;
        Transform next = NextFreePosition();

        if (next) {
            if (next.GetComponent<Position>().active) {
                selPrefab = enemyPrefabs[Random.Range(1, 4)];
                Instantiate(selPrefab, next.position, Quaternion.identity, next);
            } else {
                selPrefab = enemyPrefabs[0];
                Instantiate(selPrefab, next.position, Quaternion.identity, next);

            }
        }
        if (NextFreePosition()) Invoke("SpawnNextPosition", spawnDelay);
    }

    private void SetMovementConstraints() {
        float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));

        xMax = rightBoundary.x - width / 2;
        xMin = leftBoundary.x + width / 2;
    }

    public void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector2(width, height));
    }

}
