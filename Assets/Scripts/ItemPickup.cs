using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

    private AudioManager Audio;
    public int item_ID;
    public int item_count;

    void Start()
    {
        Audio = FindObjectOfType<AudioManager>();
    }

	private void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.Z)&&col.tag=="Player")
        {
            Audio.play("pick");
            inventory.instance.getItem(item_ID, item_count);
            Destroy(this.gameObject);
        }
       
    }
}
