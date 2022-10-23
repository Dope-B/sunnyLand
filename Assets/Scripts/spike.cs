using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour {

private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            PlayerStat.instance.Hit(10);
            player_movement.player.damaged();
        }
    }
}
