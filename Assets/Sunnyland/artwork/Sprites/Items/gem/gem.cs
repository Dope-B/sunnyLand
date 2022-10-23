using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour {

    public GameObject blink;
    private AudioManager Audio;
    public string Sound;
    // Use this for initialization
    void Start () {
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            Audio.play(Sound);
             Instantiate(blink, this.transform.position, Quaternion.identity);
             Destroy(this.gameObject);
        }
        
    }
}
