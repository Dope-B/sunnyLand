using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coll_ground : MonoBehaviour {

    rat rat;

	// Use this for initialization
	void Start () {
        rat = transform.GetComponentInParent<rat>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "ground")
        {
            if (rat.state == 0)
            {
                rat.state = 1;
            }
            else { rat.state = 0; }
        }
        
        
    }
}
