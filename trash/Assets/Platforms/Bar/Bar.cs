using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour{
    public bool vertical;
    public float distance;
    public float timeScalar;
    public float timeOffset;
    public Vector2 offset;
    Rigidbody2D rb;
    Vector2 startPos;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        startPos = new Vector2(transform.position.x, transform.position.y) + offset;
    }
    void FixedUpdate(){
        Vector2 func;
        if (vertical) {
            func = (Vector2.up * Mathf.Sin((Time.time * Mathf.PI * timeScalar) + timeOffset) * distance) + startPos + offset;
        } else {
            func = (Vector2.right * Mathf.Sin((Time.time * Mathf.PI * timeScalar) + timeOffset) * distance) + startPos + offset;
        }
        rb.MovePosition(func);
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector2 pos;
        if(startPos.x == 0 && startPos.y == 0) {
            pos = new Vector2(transform.position.x, transform.position.y) + offset;
        } else {
            pos = startPos + offset;
        }
        if (vertical) {
            Gizmos.DrawLine(pos, pos + (Vector2.up * distance));
            Gizmos.DrawLine(pos, pos + (Vector2.down * distance));
        } else {
            Gizmos.DrawLine(pos, pos + (Vector2.right * distance));
            Gizmos.DrawLine(pos, pos + (Vector2.left * distance));
        }
    }
}
