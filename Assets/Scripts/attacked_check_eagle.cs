using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacked_check_eagle : MonoBehaviour {

    public GameObject enemy_death;
    private AudioManager Audio;
    GameObject eagle;
    // Use this for initialization
    void Start () {
        eagle = transform.parent.gameObject;
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player_leg")
        {
            Audio.play("monster");
            Instantiate(enemy_death, this.transform.position, Quaternion.identity);
            Destroy(eagle.gameObject);
        }
    }
}
