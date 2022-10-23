using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_cam_mamager : MonoBehaviour {

    static C_cam_mamager C_cam;

	// Use this for initialization
	void Start () {
        if (C_cam == null)
        {
            C_cam = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(this.gameObject); }
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
