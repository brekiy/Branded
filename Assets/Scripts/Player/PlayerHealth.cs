using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Slider healthSlider;
    public Image damageFlash;
    public Color flashColor = new Color(1f, 0f, 0f, 0.25f);
    private int curHealth = 100;
    public static bool isDead = false;

    PlayerControls playerControls;
    // PlayerAudio player_audio;
    int MAX_HEALTH = 250;
    bool hurt;

    // Start is called before the first frame update
    void Start() {
        playerControls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update() {
        if (hurt) damageFlash.color = flashColor;
        else damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, 5f * Time.deltaTime);
        hurt = false;
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            // player_audio.PlayHurtSound();
            hurt = true;
        }
        curHealth += amount;
        if (curHealth > MAX_HEALTH) curHealth = MAX_HEALTH;
        healthSlider.value = curHealth;
        if (curHealth <= 0) Death();
    }

    public int GetHealth() {
        return curHealth;
    }

    void Death() {
        isDead = true;
        // player_audio.PlayDieSound();
        playerControls.SetDisabledInput(true);
    }
}
