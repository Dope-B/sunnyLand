using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant : MonoBehaviour {

    public bool isattack;
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            if (!isattack)
            {
                PlayerStat.instance.Hit(10);
            }
            else
            {
                PlayerStat.instance.Hit(20);
            }
            player_movement.player.damaged();
        }
        
    }
}
