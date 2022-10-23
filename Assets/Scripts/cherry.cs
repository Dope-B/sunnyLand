using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : MonoBehaviour {

    public GameObject blink;
    private AudioManager Audio;
    // Use this for initialization
    void Start () {
        Audio = FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Instantiate(blink, this.transform.position, Quaternion.identity);
            if (PlayerStat.instance.hp >= PlayerStat.instance.current_hp + 50)
            {
                PlayerStat.instance.current_hp += 50;
            }
            else { PlayerStat.instance.current_hp = PlayerStat.instance.hp; }
            Audio.play("star");
            DataBase.instance.floatingText(80, "red");
            Destroy(this.gameObject);
        }

    }
}
