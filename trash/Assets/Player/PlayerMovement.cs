using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Behaviour")]
    public float maxHealth;
    HealthBar healthBar;
    float health;
    public bool dead;
    public float speed;
    public float jumpSpeed;
    public Vector2 knockbackSpeed;
     float knockback;
    public float damage;
    public float reloadHeight;
    Rigidbody2D rb;
    public float direction;
    public bool finished;
    bool moving;
    bool bounceCheck;

    [Header("Hands")]
    public Vector2 handSize;
    public Vector2 handsOffset;
    public float wallSlideSpeed;
    bool facingRight = true;
    bool onWall;

    [Header("Legs")]
    public float legOffset;
    public float legLength;
    public bool onGround;

    [Header("Animation")]
    public float animSensetivity;
    public float hitAnimTime;
    float hitTime;
    bool hit;
    bool animHit;
    Animator animator;
    public AudioSource hitSound;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<HealthBar>();
        health = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }
    void Update() {
        //reload
        if (rb.position.y < reloadHeight || health <= 0) {
            dead = true;
            healthBar.setHealth(0);
            hitSound.Play();
            Destroy(gameObject);
        }
        //Casts 2 rays downwards to check if the player hits the ground
        onGround = Physics2D.Raycast(transform.position + (Vector3.left * legOffset), Vector2.down, legLength, LayerMask.GetMask("Ground")) ||
                   Physics2D.Raycast(transform.position + (Vector3.right * legOffset), Vector2.down, legLength, LayerMask.GetMask("Ground"));
        bounceCheck = Physics2D.Raycast(transform.position + (Vector3.left * legOffset), Vector2.down, legLength, LayerMask.GetMask("Bounce")) ||
                   Physics2D.Raycast(transform.position + (Vector3.right * legOffset), Vector2.down, legLength, LayerMask.GetMask("Bounce"));
        //checks if touching wall
        if(facingRight) {
            onWall = Physics2D.BoxCast(transform.position + (Vector3.up * handsOffset.y), handSize, 0, Vector2.right, handsOffset.x,LayerMask.GetMask("Ground", "Bounce"));
        }else{
            onWall = Physics2D.BoxCast(transform.position + (Vector3.up * handsOffset.y), handSize, 0, Vector2.left, handsOffset.x, LayerMask.GetMask("Ground", "Bounce"));
        }
        //wall-slide
        if (onWall && !onGround) {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !finished) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        //bounce
        if (bounceCheck) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        //animation
        if (Mathf.Abs(direction) > animSensetivity) {
            moving = true;
        } else {
            moving = false;
        }
        //hit
        if (hit && !animHit) {
            hitTime = Time.time + hitAnimTime;
            hit = false;
            animHit = true;
            hitSound.Play();
        }
        if (Time.time >= hitTime && animHit) {
           animHit = false;
        }
        animator.SetBool("Hit", animHit);
        animator.SetBool("Moving", moving);
    }
    void FixedUpdate() {
        if (!finished) {
            direction = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2((direction * speed) + knockback, rb.velocity.y);
            //Rotate the game object around the y axis when going left or right
            if (direction > 0) {
                transform.rotation = Quaternion.Euler(Vector3.zero);
                facingRight = true;
            } else if (direction < 0) {
                transform.rotation = Quaternion.Euler(Vector3.up * 180);
                facingRight = false;
            }
            knockback = 0;
        } else {
            rb.velocity = rb.velocity / 2;
            direction = 0;
        }
    }
    public void applyDamage(float damage) {
         health -= damage;
         hit = true;
         healthBar.setHealth(health);
    }
    public void heal(float heal) {
        health += heal;
        if(health >= maxHealth) {
            health = maxHealth;
        }
        healthBar.setHealth(health);
    }
    public void knockbackY() {
        rb.velocity = new Vector2(rb.velocity.x, knockbackSpeed.y);
    }
    public void knockbackX(bool onRight) {
        if (onRight) {
            knockback = knockbackSpeed.x;
        } else {
            knockback = -knockbackSpeed.x;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        //left leg
        Gizmos.DrawLine(transform.position + (Vector3.left * legOffset), 
                        transform.position + (Vector3.left * legOffset) + (Vector3.down * legLength));
        //right leg
        Gizmos.DrawLine(transform.position + (Vector3.right * legOffset),
                        transform.position + (Vector3.right * legOffset) + (Vector3.down * legLength));
        //hands
        if(facingRight) {
            Gizmos.DrawCube(transform.position + new Vector3(handsOffset.x, handsOffset.y) + (Vector3.right * handSize.x/2), handSize);
        }else {
            Gizmos.DrawCube(transform.position + (Vector3.up * (handsOffset.y)) + (Vector3.left * (handsOffset.x + handSize.x/2)), handSize);
        }
    }
}
