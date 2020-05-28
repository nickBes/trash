using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    public float[] jump;
    public float legOffset;
    public float legLength;
    public float handsLength;
    public float animSensetivity;

    Animator animator;
    Rigidbody2D rb;
    bool onGround;
    bool onWall;
    int jumpsLeft;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate(){
        movement();
        jumping();
        print(rb.velocity);
        bool touchingWall = Physics2D.Raycast(transform.position, Vector3.left, handsLength, LayerMask.GetMask("Ground")) ||
                            Physics2D.Raycast(transform.position, Vector3.right, handsLength, LayerMask.GetMask("ground"));
        if(touchingWall && rb.velocity.y == 0) {
            onWall = true;
        } else {
            onWall = false;
        }
        animator.SetBool("OnWall", onWall);
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
            jumpsLeft--;
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

        bool moving; 
        if(Mathf.Abs(direction) >= animSensetivity) {
            moving = true;
        } else {
            moving = false;
        }

        animator.SetBool("Moving", moving);
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
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * handsLength));
        //right hand
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * handsLength));

    }
}
