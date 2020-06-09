using UnityEngine;

public class Plant : MonoBehaviour{
    public GameObject plantBullet;
    Animator animator;
    [Header("Behaviour")]
    public float health;
    public float collisionDmg;
    public bool facingRight;
    public float hitAnimTime;
    float hitTime;
    bool hit;
    bool animHit;
    [Header("Shooting")]
    public float bulletSpeed;
    public float bulletDamage;
    public float rayLength;
    public float shootDistance;
    public float timeBetweenShots;
    public float shootAnimTime;
    public Vector2 bulletOffset;
    public AudioSource shootSound;
    float timeToShoot;
    bool playerFound;
    bool shooting;
    bool hasShot;
    void Start(){
        if (facingRight) {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
        animator = GetComponent<Animator>();
        hasShot = true;
    }

    void Update(){
        if (facingRight) {
            playerFound = Physics2D.Raycast(transform.position, Vector2.right, rayLength, LayerMask.GetMask("Player"));
        } else {
            playerFound = Physics2D.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Player"));
        }
        if (playerFound && hasShot) {
            hasShot = false;
            timeToShoot = Time.time + timeBetweenShots + shootAnimTime;
        }
        if(Time.time >= timeToShoot - shootAnimTime && Time.time <= timeToShoot) {
            shooting = true;
        } else {
            shooting = false;
        }
        animator.SetBool("Shooting", shooting);
        if(Time.time >= timeToShoot - (shootAnimTime/2) && !hasShot) {
            shootSound.Play();
            hasShot = true;
            GameObject temp = Instantiate(plantBullet, transform.position + new Vector3(bulletOffset.x, bulletOffset.y), Quaternion.identity);
            temp.GetComponent<PlantBullet>().travelDistance = shootDistance;
            temp.GetComponent<PlantBullet>().damage = bulletDamage;

            Vector2 tempSpeed;
            if (facingRight) {
                tempSpeed = Vector2.right * bulletSpeed;
            } else {
                tempSpeed = Vector2.left * bulletSpeed;
            }
            temp.GetComponent<Rigidbody2D>().velocity = tempSpeed;
        }
        if (hit && !animHit) {
            hitTime = Time.time + hitAnimTime;
            hit = false;
            animHit = true;
        }
        if (Time.time >= hitTime && animHit) {
            animHit = false;
        }
        animator.SetBool("Hit", animHit);
    }
    public void applyDamage(float damage) {
        health -= damage;
        hit = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.applyDamage(collisionDmg);
            if (collision.gameObject.transform.position.x > transform.position.x) {
                pm.knockbackX(true);
            } else {
                pm.knockbackX(false);
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        //visualizes the ray that detects the player
        if (facingRight) {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * rayLength));
        } else {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * rayLength));
        }
        //visualizes wher the bullet comes from
        Gizmos.DrawSphere(transform.position + new Vector3(bulletOffset.x, bulletOffset.y), .05f);
    }
}
