using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floating : MonoBehaviour {

    private const int VER = 1;
    private const int HOR = 2;
    public int direction;
    public int currentState = 0;// Left or Up=0- Right or Down=1
    public int duration;
    private WaitForSeconds wait = new WaitForSeconds(0.01f);
    private player_movement player;
    public bool activated = false;
    private bool onPlayer = false;
    private bool alreadyRunning = false;
    public float speed = 0.01f;
    private int counter = 0;
    private Vector3 vector;
	
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            onPlayer = false;
        }
    }
    void Start()
    {
        player = FindObjectOfType<player_movement>();
    }
	// Update is called once per frame
	void Update () {
        if (!alreadyRunning)
        {
            if (activated)
            {
                StartCoroutine(move());
                alreadyRunning = true;
            }
        }
        if (!activated)
        {
            StopAllCoroutines();
            alreadyRunning = false;
            vector = new Vector3(0, 0, 0);
        }

	}
    IEnumerator move()
    {
        switch (direction)
        {
            case VER:
                if (currentState == 1)
                {
                    vector = new Vector3(-speed, 0, 0);
                    for (; counter < duration; counter++)
                    {
                        transform.position += vector;
                        if (onPlayer)
                        {
                            player.transform.position += vector;
                        }
                        yield return wait;
                    }
                    currentState = 0;
                    counter = 0;
                    alreadyRunning = false;
                }
                else
                {
                    vector = new Vector3(speed, 0, 0);
                    for (; counter < duration; counter++)
                    {
                        transform.position += vector;
                        if (onPlayer)
                        {
                            player.transform.position += vector;
                        }
                        yield return wait;
                    }
                    currentState = 1;
                    counter = 0;
                    alreadyRunning = false;
                }
                break;
            case HOR:
                if (currentState == 1)
                {
                    vector = new Vector3(0, -speed, 0);
                    for (; counter < duration; counter++)
                    {
                        transform.position += vector;
                        if (onPlayer)
                        {
                            player.transform.position += vector;
                        }
                        yield return wait;
                    }
                    currentState = 0;
                    counter = 0;
                    alreadyRunning = false;
                }
                else
                {
                    vector = new Vector3(0, speed, 0);
                    for (; counter < duration; counter++)
                    {
                        transform.position += vector;
                        if (onPlayer)
                        {
                            player.transform.position += vector;
                        }
                        yield return wait;
                    }
                    currentState = 1;
                    counter = 0;
                    alreadyRunning = false;
                }
                break;
        }
    }
}
