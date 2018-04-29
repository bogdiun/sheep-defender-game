using System.Collections;
using UnityEngine;

// as IFireable, IProjectile, IHasTarget, etc.?
public class HomingGun : MonoBehaviour, IFireable {
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

    public void Fire(string targetTag) {
        this.targetTag = targetTag;
        if (!isFiring) StartCoroutine(Delay(firingDelay));
    }

    public void Stop() {
        behaviour.enabled = false;
        isFiring = false;
    }

    private IEnumerator Delay(float time) {
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().applyRootMotion = true;
        behaviour.Init(firingForce, targetTag);
        isFiring = true;
        //gameObject.layer = GameObject.FindGameObjectWithTag(targetTag).layer - 1;
    }
}