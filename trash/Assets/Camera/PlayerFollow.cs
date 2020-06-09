using UnityEngine;

public class PlayerFollow : MonoBehaviour{
    public Transform target;
    public float smooth = .25f;
    public Vector3 offset;
    void Awake() {
        transform.position = target.position + offset;
    }
    void FixedUpdate(){
        if(target != null) {
            Vector3 progress = Vector3.Lerp(transform.position, target.position + offset, smooth);
            transform.position = progress;
        }
    }
}
