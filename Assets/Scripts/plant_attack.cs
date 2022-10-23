using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant_attack : MonoBehaviour {

    private Animator ani;
    private plant plant;
    private player_movement player;
    private AudioManager Audio;
    // Use this for initialization
    void Start () {
        ani = GetComponentInParent<Animator>();
        plant = GetComponentInParent<plant>();
        player = FindObjectOfType<player_movement>();
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            if (player.transform.position.x >= plant.transform.position.x)
            {
                plant.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                plant.transform.localScale = new Vector3(1, 1, 1);
            }
            plant.isattack = true;
            ani.SetBool("readyToattack", true);
            Audio.play("bite", 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            plant.isattack = false;
            ani.SetBool("readyToattack", false);
        }
    }
}
