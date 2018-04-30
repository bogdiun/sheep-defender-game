using System.Collections;
using UnityEngine;

// as IFireable, IProjectile, IHasTarget, etc.?
public class BaseGun : MonoBehaviour, IShoot {
    public Transform shotSpawn;
    public Rigidbody2D shotPrefab;
    public AudioClip audioClip;

    public float firingForce;
    public float firingRate;
    public float firingDelay;
    private bool isFiring = false;

    private string target;

    //  -- Behaviours --
    public bool isRandom = false;   //instead make RandomBehaviour?
    public bool isHoming = false;
    //spray
    //etc.

    // private void Start() {
    //     shotSpawn = transform.GetChild(0);
    // }

    public void Shoot(string target) {
        // Debug.Log("Target[" + LayerMask.LayerToName(gameObject.layer) + "]:" + target);
        this.target = target;
        if (!isFiring) {
            isFiring = true;
            InvokeRepeating("Spawn", firingDelay, firingRate);
        }
    }

    public void Spawn() {
        Rigidbody2D shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as Rigidbody2D;
        shot.gameObject.layer = gameObject.layer + 1;

        Vector2 direction = shot.transform.up;
        // add behaviours to the gun
        if (isRandom) {
            direction += new Vector2(Random.Range(-0.5f, 0.5f), 0f);
            float rot = Vector3.Cross(direction, shot.transform.up).z;
            shot.angularVelocity = -rot * 180f;
        }

        //separate movement?    
        if (isHoming && (this.tag == "Enemy")) {
            HomingBehaviour homing = shot.gameObject.GetComponent<HomingBehaviour>();
            if (!homing) {
                homing = shot.gameObject.AddComponent<HomingBehaviour>();
            }

            homing.Init(firingForce, target);

        } else shot.velocity = direction * firingForce;

        if (audioClip) AudioSource.PlayClipAtPoint(audioClip, transform.position, 0.3f);
    }

    public void Stop() {
        CancelInvoke();
        isFiring = false;
    }

    private IEnumerator Delay(float time) {
        yield return new WaitForSeconds(time);
        isFiring = true;
    }
}