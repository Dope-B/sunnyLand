using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacked_check : MonoBehaviour {

    rat rat;
    public GameObject enemy_death;
    private AudioManager Audio;
    public string deathSound;

	// Use this for initialization
	void Start () {
        rat = transform.GetComponentInParent<rat>();
        Audio = FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "player_leg") {
            Instantiate(enemy_death, this.transform.position, Quaternion.identity);
            Audio.play(deathSound);
            Destroy(rat.gameObject);
        }
    }
}
