using UnityEngine;

public class Shredder : MonoBehaviour {
    public LayerMask toShred;

    void OnTriggerExit2D(Collider2D other) {
        if (toShred == (toShred | (1 << other.gameObject.layer))) {
            Destroy(other.gameObject);
            Debug.Log("Shredding " + other.name);
        }
    }
}