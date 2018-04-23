using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other) {
        Debug.Log(other.name+" has been Shredded.");
        Destroy(other.gameObject);
    }
}