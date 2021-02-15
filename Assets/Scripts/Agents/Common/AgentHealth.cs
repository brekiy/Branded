using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentHealth : MonoBehaviour {
    public Color flashColor = new Color(1f, 0f, 0f, 0.25f);
    public int MAXHEALTH = 250;
    public int health = 100;

    protected bool hurt;

    private Color damageFlash;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (hurt) damageFlash = flashColor;
        else damageFlash = Color.Lerp(damageFlash, Color.clear, 5f * Time.deltaTime);
        hurt = false;
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            Hurt();
        }
        health += amount;
        if (health > MAXHEALTH) health = MAXHEALTH;
        if (health <= 0) Death();
    }

    public void Hurt() {
        hurt = true;
    }

    public void Death() {
        // die lol
        Destroy(gameObject);
    }
}
