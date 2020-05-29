using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float[] jump;
    public float animSensetivity;

    [Header("Legs")]
    public float legOffset;
    public float legLength;

    [Header("Hands")]
    public GameObject hand;
    public float handSize;
    public float handOffsetL;
    public float handOffsetR;

    [Header("Wall")]
    public bool onWallLeft;
    public bool onWallRight;
    public float wallFallSpeed;

    Animator animator;
    Rigidbody2D rb;
    bool onGround;
    bool moving;
    bool onWall;
    int jumpsLeft;
    int previousWall;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        createHands();
    }
    void FixedUpdate(){
        movement();
        wallJump();
        jumping();
        animator.SetBool("Moving", moving);
        animator.SetBool("OnWall", onWall);
    }
    void createHands() {
        GameObject leftHand = Instantiate(hand, transform.position + (Vector3.left * handOffsetL), Quaternion.identity, transform);
        leftHand.GetComponent<BoxCollider2D>().size = Vector2.one * handSize;
        GameObject rightHand = Instantiate(hand, transform.position + (Vector3.right * handOffsetR), Quaternion.identity, transform);
        rightHand.GetComponent<BoxCollider2D>().size = Vector2.one * handSize;
    }
    void wallJump() {
        //wall jump
        if (onWallLeft) {
            if(previousWall == 1 && jumpsLeft < 1) {
                jumpsLeft = 1;
            }
            previousWall = 0;
        }else if (onWallRight) {
            if (previousWall == 0 && jumpsLeft < 1) {
                jumpsLeft = 1;
            }
            previousWall = 1;
        }
        onWall = onWallLeft || onWallRight;
        if (Mathf.Abs(rb.velocity.x / speed) >= animSensetivity) {
            if (onWall) {
                moving = false;

                if (rb.velocity.y == 0) {
                    rb.velocity = new Vector2(rb.velocity.x, -wallFallSpeed);
                }
            } else {
                moving = true;
            }
        } else {
            moving = false;
            onWall = false;
        }
    }
    void jumping() {
        onGround = Physics2D.Raycast(transform.position + (Vector3.left * legOffset), Vector2.down, legLength, LayerMask.GetMask("Ground")) ||
                   Physics2D.Raycast(transform.position + (Vector3.right * legOffset), Vector2.down, legLength, LayerMask.GetMask("Ground"));
        if (onGround) {
            jumpsLeft = jump.Length;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0) {
            float jumpStrength = jump[jumpsLeft - 1];
            rb.velocity = Vector2.up * jumpStrength;
            if (onWallLeft) {
                jumpsLeft = 1;
            } else {
                jumpsLeft--;
            }
        }
    }
    void movement() {
        float direction = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if(direction > 0) {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        } else if(direction < 0){
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
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
        //left hand
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * handOffsetL) + (Vector3.left * handSize / 2));
        //right hand
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * handOffsetR) + (Vector3.right * handSize / 2));
    }
}
