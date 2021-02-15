using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    public float speed = 4.5f;
    public float minDistance = 0;

    Transform playerTransform;
    AgentHealth agentHealth;
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start() {
        agentHealth = gameObject.GetComponent<AgentHealth>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
