using UnityEngine;
using System.Collections;

// once player is defeated make one of the enemy ships desert and become the player
public class PlayerController : MonoBehaviour {
    // private GunSystem gunSystem; temporarily will be built in 
    private LevelManager levelManager;
    private IFireable[] weapons;
    public GameObject fxDestroyed;

    private IFireable primary;
    private IFireable secondary;

    public float hitPoints;
    public float moveSpeed;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    void Start() {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        weapons = GetComponents<IFireable>();
        primary = weapons[0];
        secondary = weapons[1];

        SetMovementConstraints();
    }

    void FixedUpdate() {
        //movement
        Vector2 localPosition = transform.position;

        localPosition.y += Time.deltaTime * moveSpeed * 0.8f * Input.GetAxisRaw("Vertical");
        localPosition.x += Time.deltaTime * moveSpeed * Input.GetAxisRaw("Horizontal");

        float xClamp = Mathf.Clamp(localPosition.x, xMin, xMax);
        float yClamp = Mathf.Clamp(localPosition.y, yMin, yMax);

        transform.position = new Vector3(xClamp, yClamp, 0);
    }

    private void Update() {
        // Primary Gun
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            primary.Fire("Enemy");

        } else if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) {
            primary.Stop();
        }

        // Secondary Gun
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1)) {
            secondary.Fire("Enemy");

        } else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetMouseButtonUp(1)) {
            secondary.Stop();
        }
    }

    private void SetMovementConstraints() {
        float cameraDistance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));

        xMax = rightBoundary.x - 1f;
        xMin = leftBoundary.x + 1f;

        // do not hardcode.. but
        yMax = 3f;
        yMin = 0.2f;

    }

    private void OnTriggerEnter2D(Collider2D other) {
        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile) {
            hitPoints = hitPoints - projectile.Damage();
            StartCoroutine(ColorDamaged());
            Destroy(other.gameObject);
            if (hitPoints <= 0) {
                //unlink all relations to player 
                Instantiate(fxDestroyed, transform.position, Quaternion.identity);
                levelManager.LoadLevel("Lose");
                // Destroy(gameObject);
                // StartCoroutine(levelManager.LoadLevel("Lose", 1f));

            }
        }
    }

    IEnumerator ColorDamaged() {
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.2f, 0.3f);
        yield return new WaitForSeconds(0.18f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
