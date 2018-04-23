using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {
    public bool active;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
//TODO: Refactor to Spawn Point/Position, place the enemy prefab in this class
//      then in FormationController get this class and instantiate a prefab from this here?
//      ie. position.Spawn(), or position.active = true, which would then update itself here..


//      pro: easier to design formations
//      pro: can add stuff like delayed spawn based on particular position in formation
//      pro: allows extending, various formation position implementations, better than tags

//      con: messier if I want to determine in code what is spawned
//      random and dynamic formation behaviour can be implemented still if needed with parameters.
}
