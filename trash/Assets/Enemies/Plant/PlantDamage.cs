using UnityEngine;

public class PlantDamage : MonoBehaviour{
    public Plant plant;
    public GameObject death;
    public AudioSource hitAudio;
    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            if (!pm.onGround) {
                plant.applyDamage(pm.damage);
                hitAudio.Play();
                //knockback
                pm.knockbackY();
            }
            //death
            if(plant.health <= 0) {
                Instantiate(death, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject );
            }
        }
    }
}
