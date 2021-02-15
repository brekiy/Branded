using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : AgentHealth {
    public Slider healthSlider;
    // public Image screenDamageFlash;

    PlayerControls playerControls;
    // PlayerAudio player_audio;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (hurt) screenDamageFlash.color = flashColor;
        // else screenDamageFlash.color = Color.Lerp(screenDamageFlash.color, Color.clear, 5f * Time.deltaTime);
        // hurt = false;
    }

}
