using UnityEngine;

public class Spikes : MonoBehaviour{
    public GameObject spikes;
    public float xUnits;
    public float damage;
    public Vector2 offset;
    public Vector2 spriteSize { 
        get {
            return new Vector2(spikes.GetComponent<SpriteRenderer>().sprite.bounds.size.x
                              ,spikes.GetComponent<SpriteRenderer>().sprite.bounds.size.y);
        } 
    }
    public Vector2 size { 
        get { 
            return new Vector2(spriteSize.x * xUnits,
                               spriteSize.y); 
        } 
    }
    void Start(){
        float xDif = 0;
        Vector3 pos = transform.position - (Vector3.right * (size.x/2)) + (Vector3.right * (spriteSize.x/2));
        for(int i = 0; i < xUnits; i++) {
            Instantiate(spikes, 
                        pos + (xDif * Vector3.right), 
                        Quaternion.identity, 
                        transform);
            xDif += size.x / xUnits;
        }
        GetComponent<BoxCollider2D>().size = new Vector2(size.x, size.y) + (Vector2.up * offset.y);
        GetComponent<BoxCollider2D>().offset = Vector2.up * (offset.y/2) + Vector2.right * (offset.x);
    }
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            pm.applyDamage(damage);
            if (pm.onGround) {
                if(pm.gameObject.transform.position.x > transform.position.x) {
                    pm.knockbackX(true);
                } else {
                    pm.knockbackX(false);
                }
            } else {
                pm.knockbackY(); 
            }
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Vector3 bottomLeft = transform.position - (Vector3.up * (size.y / 2)) - (Vector3.right * (size.x / 2)) + (Vector3.right * offset.x);
        Vector3 bottomRight = transform.position - (Vector3.up * (size.y / 2)) + (Vector3.right * (size.x / 2)) + (Vector3.right * offset.x);
        Vector3 topLeft = transform.position + (Vector3.up * (size.y / 2)) + (Vector3.up * offset.y) - (Vector3.right * (size.x / 2)) + (Vector3.right * offset.x);
        Vector3 topRight = transform.position + (Vector3.up * (size.y / 2)) + (Vector3.up * offset.y) + (Vector3.right * (size.x / 2)) + (Vector3.right * offset.x);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(bottomRight, topRight);
    }
}
