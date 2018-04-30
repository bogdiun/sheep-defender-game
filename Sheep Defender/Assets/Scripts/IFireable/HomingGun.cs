using System.Collections;
using UnityEngine;

// as IFireable, IProjectile, IHasTarget, etc.?
public class HomingGun : MonoBehaviour, IShoot {
    private HomingBehaviour behaviour;
    private string targetTag;

    public float firingForce;
    public float firingDelay;
    private bool isFiring = false;

    // instead of having different xGun, have object that enables behaviours like Ifireable or Ihoming?
    // target could be Player, enemies, other GO's? .. so
    private void Start() {
        behaviour = GetComponent<HomingBehaviour>();
    }

    public void Shoot(string targetTag) {
        this.targetTag = targetTag;
        if (!isFiring) StartCoroutine(Delay(firingDelay));
    }

    public void Stop() {
        behaviour.enabled = false;
        isFiring = false;
    }

    private IEnumerator Delay(float time) {
        isFiring = true;
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().applyRootMotion = true;
        behaviour.Init(firingForce, targetTag);
        gameObject.layer = gameObject.layer + 1;
    }
}