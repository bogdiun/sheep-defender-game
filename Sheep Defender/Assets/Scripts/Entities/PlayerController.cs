using UnityEngine;
using System.Collections;

// once player is defeated make one of the enemy ships desert and become the player
public class PlayerController : MonoBehaviour {
    // private GunSystem gunSystem; temporarily will be built in 
    [System.Serializable]
    public class Constraint { public Vector2 min, max; }
    public Constraint constraint;

    private LevelManager levelManager;
    public GameObject fxDestroyed;

    private IShoot[] weapons;
    // private IShoot primary, secondary;

    public float hitPoints;
    public float moveSpeed;


    void Start() {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        weapons = GetComponents<IShoot>();
        // primary = weapons[0];
        // secondary = weapons[1];

        SetMovementConstraints();
    }

    void FixedUpdate() {
        //move
        float x = Time.deltaTime * moveSpeed * Input.GetAxisRaw("Horizontal");
        float y = Time.deltaTime * moveSpeed * 0.8f * Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(x, y));

        //constrain movement
        x = Mathf.Clamp(transform.position.x, constraint.min.x, constraint.max.x);
        y = Mathf.Clamp(transform.position.y, constraint.min.y, constraint.max.y);
        transform.position = new Vector2(x, y);
    }

    private void Update() {
        // Primary Gun
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            weapons[0].Shoot("Enemy");
            weapons[2].Shoot("Enemy");

        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            weapons[0].Stop();
            weapons[2].Stop();
        }

        // Secondary Gun
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1)) {
            weapons[1].Shoot("Enemy");

        } else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetMouseButtonUp(1)) {
            weapons[1].Stop();
        }
    }

    private void SetMovementConstraints() {
        Camera camera = Camera.main;
        float cameraDistance = transform.position.z - camera.transform.position.z;  //unnecessary in ortho
        constraint.min = Vector2.one + (Vector2) camera.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        constraint.max = -Vector2.one + (Vector2) camera.ViewportToWorldPoint(new Vector3(1, 1, cameraDistance));

        constraint.max.y = constraint.max.y * 0.3f; //30% of camera view height
    }

    private void OnTriggerEnter2D(Collider2D other) {
        IDamage hit = other.GetComponent<IDamage>();
        if (hit != null) {
            hitPoints -= hit.Damage();
            StartCoroutine(ColorDamaged());
        }

        Destroy(other.gameObject);
        if (hitPoints <= 0) {
            //unlink all relations to player 
            Instantiate(fxDestroyed, transform.position, transform.rotation);
            levelManager.LoadLevel("Lose");
            // Destroy(gameObject);
            // StartCoroutine(levelManager.LoadLevel("Lose", 1f));

        }
    }

    IEnumerator ColorDamaged() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(0.9f, 0.2f, 0.3f);

        yield return new WaitForSeconds(0.18f);
        sprite.color = Color.white;
    }
}
