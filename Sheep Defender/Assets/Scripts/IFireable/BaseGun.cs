using System.Collections;
using UnityEngine;

// as IFireable, IProjectile, IHasTarget, etc.?
public class BaseGun : MonoBehaviour, IFireable {
    public Rigidbody2D projectilePrefab;
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

    public void Fire(string target) {
        // Debug.Log("Target[" + LayerMask.LayerToName(gameObject.layer) + "]:" + target);
        this.target = target;

        // if (isRandom) {

        //     if (isFiring) {
        //         float probability = Time.deltaTime * firingRate;
        //         if (Random.value < probability) Spawn();

        //     } else StartCoroutine(Delay(firingDelay));
        
        // } else 
        if (!isFiring) {
            isFiring = true;
            InvokeRepeating("Spawn", firingDelay, firingRate);
        }
    }

    public void Spawn() {
        Vector3 spawnPoint = transform.position;

        Rigidbody2D projectile = Instantiate(projectilePrefab, spawnPoint, Quaternion.identity) as Rigidbody2D;
        projectile.gameObject.layer = gameObject.layer + 1;

        Vector2 direction = projectile.transform.up;

        if (target == "Player") {
            projectile.transform.Rotate(0f, 0f, 180f);
            direction = projectile.transform.up;
        }
        projectile.transform.position += projectile.transform.up;

        // add behaviours to the gun
        if (isRandom) {
            direction += new Vector2(Random.Range(-0.5f, 0.5f), 0f);
            float rot = Vector3.Cross(direction, projectile.transform.up).z;
            projectile.angularVelocity = -rot * 180f;
        }

        if (isHoming && (this.tag == "Enemy")) {
            HomingBehaviour homing = projectile.gameObject.GetComponent<HomingBehaviour>();
            if (!homing) {
                homing = projectile.gameObject.AddComponent<HomingBehaviour>();
            }

            homing.Init(firingForce, target);

        } else projectile.velocity = direction * firingForce;

        // Debug.Log(this.name + ": " + projectile.name + " pew pew at " + projectile.gameObject.layer);
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