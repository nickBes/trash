using UnityEngine;

public class Flag : MonoBehaviour{
    public float animTime;
    AudioSource sound;
    Animator animator;
    float nextTime;
    bool anim;
    bool idle;
    bool on;
    void Start() {
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void Update() {
        if (anim) {
            nextTime = Time.time + animTime;
            anim = false;
            idle = true;
        }
        if(Time.time >= nextTime && idle) {
            idle = false;
            on = true;
        }
        animator.SetBool("Start", idle);
        animator.SetBool("Idle", on);
    }
    void OnTriggerEnter2D(Collider2D collision) {
        if (!on && !idle) {
            if (collision.gameObject.tag == "Player") {
                sound.Play();
                anim = true;
                collision.GetComponent<PlayerMovement>().finished = true;
            }
        }
    }
}
