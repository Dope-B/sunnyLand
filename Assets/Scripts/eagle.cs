using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eagle : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            PlayerStat.instance.Hit(15);
            player_movement.player.damaged();
        }
    }
}
