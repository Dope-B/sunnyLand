using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crank : MonoBehaviour {

    Animator animator;
    private AudioManager Audio;
    public floating[] floatings;
    private bool activated=false;
    private bool isworking;

	void OnTriggerStay2D(Collider2D col)
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (col.tag == "Player"&&!isworking)
            {
                isworking = true;

                activated = !activated;
                if (activated)
                {
                    animator.SetBool("isUp", true);
                }
                else
                {
                    animator.SetBool("isUp", false);
                }
                Audio.play("crank");
                StartCoroutine(cooldown());
            }
        }
    }
	void Start () {
        animator = GetComponent<Animator>();
        Audio = FindObjectOfType<AudioManager>();
    }
	IEnumerator cooldown()
    {
        yield return new WaitForSeconds(0.01f);
        isworking = false;
    }
	
	void Update () {
        if (activated)
        {
            for (int i = 0; i < floatings.Length; i++)
            {
                floatings[i].activated = true;
            }
        }
        else
        {
            for (int i = 0; i < floatings.Length; i++)
            {
                floatings[i].activated = false;
            }
        }
    }
}
