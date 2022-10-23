using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack_check : MonoBehaviour {

    public GameObject enemy_death;
    private AudioManager Audio;
    frog frog;
    // Use this for initialization
    void Start () {
        frog = transform.GetComponentInParent<frog>();
        Audio = FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player_leg")
        {
            Instantiate(enemy_death, this.transform.position, Quaternion.identity);
            Audio.play("frog");
            Destroy(frog.gameObject);
        }
    }
}
