using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPositionSetting : MonoBehaviour {

    player_movement player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
    }

    void OnTriggerStay2D(Collider2D col) {
        
        if (player.GSanimator.GetBool("isClimbing"))
        {
            if (col.tag == "Player") {
                        player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
                    }
        }
        
    }
}
