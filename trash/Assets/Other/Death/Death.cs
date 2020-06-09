using UnityEngine;

public class Death : MonoBehaviour{
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
