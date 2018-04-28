using System.Collections;
using UnityEngine;

// as IFireable, IProjectile, IHasTarget, etc.?
public class BodyGun : MonoBehaviour, IFireable {
    public float firingForce;
    public float firingDelay;
    private bool isFiring = false;
    private string targetTag;

    // instead of having different xGun, have object that enables behaviours like Ifireable or Ihoming?
    // target could be Player, enemies, other GO's? .. so

    public void Fire(string targetTag) {
        this.targetTag = targetTag;
        if (!isFiring) StartCoroutine(Delay(firingDelay));
    }

    public void Stop() {
        isFiring = false;
        GetComponent<HomingBehaviour>().enabled = false;
    }

    private IEnumerator Delay(float time) {
        yield return new WaitForSeconds(time);
        isFiring = true;
        GetComponent<HomingBehaviour>().Init(firingForce, targetTag);
        GetComponent<Animator>().applyRootMotion = true;
        //gameObject.layer = GameObject.FindGameObjectWithTag(targetTag).layer - 1;
    }
}