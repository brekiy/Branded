using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLayerMaskHit : MonoBehaviour {
    public string[] destroyLayers = {"Floor"};
    private int layerMask;

    // Start is called before the first frame update
    void Start() {
        layerMask = LayerMask.GetMask(destroyLayers);
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
        if (UtilFunctions.LayerMaskContains(layerMask, other.gameObject.layer)) {
            Destroy(gameObject);
        }
    }
}
