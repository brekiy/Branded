using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Vector3 parentVelocity = Vector3.zero;
    private float speed = 4f;
    private int damage = 1;

    private string[] canDamageTags = {
        "Player",
        "Enemy"   
    };

    // Start is called before the first frame update
    void Start() {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
            // Vector3.Normalize(parentVelocity) + 
            // Debug.Log("parentVelocity:" + parentVelocity);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Setup(float speed, int damage) {
        this.speed = speed;
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other) {
        // Debug.Log(other.gameObject.tag);
        if (Array.Find(canDamageTags, arrTag => string.Equals(arrTag, other.gameObject.tag)) != null) {
            AgentHealth agentHealth = other.gameObject.GetComponent<AgentHealth>();
            if (!agentHealth) {
                Debug.LogWarning("Proectile hit an agent but it didn't have a health component!");
                return;
            } else {
                agentHealth.ChangeHealth(-1 * damage);
            }
            Destroy(gameObject);
        }
    }
}
