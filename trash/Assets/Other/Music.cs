using UnityEngine;

public class Music : MonoBehaviour{
    public static Music instance;
    void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
