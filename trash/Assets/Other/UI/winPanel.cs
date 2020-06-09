using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class winPanel : MonoBehaviour {
    public Text score;
    public float animTime;
    float stopTime;
    Animator animator;
    bool loaded;
    void Start() {
        animator = GetComponent<Animator>();
        stopTime = Time.time + animTime;
    }

    void Update() {
        if (Time.time >= stopTime) {
            loaded = true;
            animator.SetBool("Idle", loaded);
        }
        if (Input.GetKeyDown(KeyCode.Space) && loaded) {
            SceneManager.LoadScene("level1");
        }
    }

    public void setScore(string score) {
        this.score.text = "Score: " + score;
    }
}
