using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundChecker : MonoBehaviour {

    private BoxCollider2D bound;
    private cam_manager cam;
    private player_movement player;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cam_manager>();
        bound = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindObjectOfType<player_movement>();
        player.mapBound = bound;
        cam.SetBound(bound);
	}
	
}
