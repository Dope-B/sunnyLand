using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rat : MonoBehaviour {

    public float speed = 6.0f;
    Vector3 vector;
    public int state =1;
	// Use this for initialization
	void Start () {
        StartCoroutine("stateChecker");
    }
	// Update is called once per frame
	void FixedUpdate () {
        move();
	}
    void move() {
        vector = Vector3.zero;
        switch (state) {
            case 0:
                vector = Vector3.left;
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case 1:
                vector = Vector3.right;
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
        transform.position += vector * speed * Time.deltaTime;

    }
    IEnumerator stateChecker() {
        state = Random.Range(0, 2);
        yield return new WaitForSeconds(2.3f);
        StartCoroutine("stateChecker");
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            PlayerStat.instance.Hit(10);
            player_movement.player.damaged();
        }
    }
}
