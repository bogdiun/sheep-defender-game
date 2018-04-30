using UnityEngine;

//Or maybe it redo as IFireable, IProjectile, IHasTarget, etc.?
public class Shield : MonoBehaviour, IShoot {
    public void Shoot(string targetTag) {
        throw new System.NotImplementedException();
    }

    public void Stop() {
        throw new System.NotImplementedException();
    }
}