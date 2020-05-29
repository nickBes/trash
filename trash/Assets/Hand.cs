using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour{
    public PlayerMovement pm;
    int direction;
    void Start(){
        pm = transform.parent.GetComponent<PlayerMovement>();
        if(transform.position.x - transform.parent.position.x < 0) {
            direction = 0;
        } else {
            direction = 1;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ground") {
            if (direction == 0) {
                pm.onWallLeft = true;
            } else {
                pm.onWallRight = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision) {
        if (direction == 0) {
            pm.onWallLeft = false;
        } else {
            pm.onWallRight = false;
        }
    }
}
