using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    Text timer_text;

    static float total_seconds;

    // Use this for initialization
    void Start () {
		timer_text = GetComponent<Text>();
        total_seconds = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!PlayerHealth.isDead) {
            total_seconds += Time.deltaTime;
        }
        timer_text.text = total_seconds.ToString("00");
	}

    public static float GetTimeSurvived() {
        return total_seconds;
    }
}
