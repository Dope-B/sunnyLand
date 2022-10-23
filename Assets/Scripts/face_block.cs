using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face_block : MonoBehaviour {


    RaycastHit2D hit;
    RaycastHit2D hit2;
    private bool isattack;
    private bool isup;
    WaitForSeconds wait = new WaitForSeconds(0.03f);
    private AudioManager Audio;
    private Vector3 vector = new Vector2(0, 0.015f);
    private Vector3 pos;
    // Use this for initialization
    void Start () {
        pos = this.transform.position;
        Audio = FindObjectOfType<AudioManager>();
    }
	
	// Update is called once per frame
	void Update () {
        hit= Physics2D.Raycast(transform.position, Vector2.down,5f,1<<11);
        hit2= Physics2D.Raycast(transform.position, Vector2.down, 0.17f, 1 << 9);
        if (hit)
        {
            if (hit.collider.tag == "Player"&&!isattack&&!isup)
            {
                StartCoroutine(down());
                isattack = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player"&&isattack&&!isup)
        {
            PlayerStat.instance.Hit(20);
            player_movement.player.damaged();
        }
    }
    IEnumerator down()
    {
        while (!hit2)
        {
            this.transform.position -= vector*3;
            yield return wait;
        }
        isup = true;
        Audio.play("stomp",0.25f);
        StartCoroutine(up());
    }
    IEnumerator up()
    {
       
        yield return new WaitForSeconds(0.6f);
        while (this.transform.position != pos)
        {
            this.transform.position += vector;
            yield return wait;
        }
        isattack = false;
        isup = false;
    }
}
