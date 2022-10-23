using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_death : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

