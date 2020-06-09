using UnityEngine;

public class PlantBullet : MonoBehaviour{
    public GameObject bulletDistruction;
    public float travelDistance;
    public float damage;
    Vector3 rootPos;
    PlayerMovement player;
    void Start() {
        rootPos = transform.position;
        if(GameObject.FindGameObjectsWithTag("Player").Length != 0) {
            player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        }
    }
    void Update(){
        if(player != null) {
            if ((transform.position - rootPos).magnitude >= travelDistance || player.finished) {
                bulletDestroy(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            player.applyDamage(damage);
            if (collision.gameObject.transform.position.x > transform.position.x) {
                player.knockbackX(true);
            } else {
                player.knockbackX(false);
            }
            bulletDestroy(true);
        }
    }
    void bulletDestroy(bool collided) {
        GameObject des = Instantiate(bulletDistruction, transform.position, Quaternion.identity);
        if (!collided) {
            des.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        }
        Destroy(gameObject);
    }
}
