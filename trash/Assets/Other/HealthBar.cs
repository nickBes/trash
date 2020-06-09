using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    Slider slider;
    public float smooth = .25f;
    float value;
    void Awake() {
        slider = GetComponent<Slider>();
    }
    void Update() {
        slider.value = slider.value + (smooth * (value - slider.value));
    }
    public void setMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;
        value = health;
    }
    public void setHealth(float health) {
        value = health;
    }
}
