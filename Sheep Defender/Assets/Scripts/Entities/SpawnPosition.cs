using UnityEngine;

public class SpawnPosition : MonoBehaviour {
    public GameObject[] prefabs;
    // delay
    // ?

    public void Spawn() {
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    //      random and dynamic formation behaviour can be implemented still if needed with parameters.
}
