using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBulletDestruction : MonoBehaviour {
    public float lifeTime;
    float deathTime;
    void Start(){
        deathTime = Time.time + lifeTime;
    }

    void Update(){
        if(Time.time >= deathTime) {
            Destroy(gameObject);
        }
    }
}
