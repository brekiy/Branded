using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 4f;
    public int damage = 1;

    private string[] canDamageTags = {
        "Player",
        "Enemy"   
    };


    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Rigidbody>().velocity =
            gameObject.transform.forward * speed;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.tag);
        if (Array.Find(canDamageTags, arrTag => string.Equals(arrTag, other.gameObject.tag)) != null) {
            AgentHealth agentHealth = other.gameObject.GetComponent<AgentHealth>();
            if (!agentHealth) {
                Debug.LogWarning("Proectile hit an agent but it didn't have a health component!");
                return;
            } else {
                agentHealth.ChangeHealth(-1 * damage);
            }
        }
    }
}
