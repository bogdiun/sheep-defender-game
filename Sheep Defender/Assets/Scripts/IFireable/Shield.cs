using System.Collections;
using UnityEngine;

//Or maybe it redo as IFireable, IProjectile, IHasTarget, etc.?
public class Shield : MonoBehaviour, IFireable {
    public void Fire(string targetTag) {
        throw new System.NotImplementedException();
    }

    public void Stop() {
        throw new System.NotImplementedException();
    }
}