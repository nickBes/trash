using UnityEngine;

public class Fruit : MonoBehaviour{
    public GameObject death;
    public float heal;
    public AudioSource collectSound;
    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            collision.GetComponent<PlayerMovement>().heal(heal);
            Instantiate(death, transform.position, Quaternion.identity);
            collectSound.Play();
            Destroy(gameObject);
        }
    }
}
