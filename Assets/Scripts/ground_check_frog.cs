using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground_check_frog : MonoBehaviour {

    frog frog;

	// Use this for initialization
	void Start () {
        frog = transform.GetComponentInParent<frog>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "ground")
        {
            frog.GS_rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "ground")
        {
            frog.GS_rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
