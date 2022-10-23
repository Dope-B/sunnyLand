using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_kill : MonoBehaviour {

    player_movement player;
    private AudioManager Audio;
    private BoxCollider2D box;
    public BoxCollider2D GSbox { get { return box; } set { box = GSbox; } }

    // Use this for initialization
    void Start () {
        player = transform.GetComponentInParent<player_movement>();
        box = gameObject.GetComponent<BoxCollider2D>();
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "enemy_top") {
            Audio.play("attack");
            player.GSrigid.velocity = Vector3.zero;
            player.GSrigid.AddForce(new Vector2(0, 1.8f),ForceMode2D.Impulse);
        }
    }
}
