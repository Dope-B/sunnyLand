using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_trigger_eagle : MonoBehaviour {

    GameObject eagle;
    player_movement player;
    AudioManager Audio;
    public string triggerSound;
    private float sinY;
    private float cosX;

    // Use this for initialization
    void Start () {
        eagle = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void move()
    {
        sinY = eagle.transform.position.y - player.transform.position.y;
        cosX = eagle.transform.position.x - player.transform.position.x;
        eagle.transform.position += new Vector3(-cosX/80, -sinY/80);
        if (cosX < 0)
        {
            eagle.transform.localScale = new Vector3(-1, 1, 0);
        }
        else { eagle.transform.localScale = new Vector3(1, 1, 0); }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body") { move(); }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player_body") { Audio.play(triggerSound); }
    }
}
