using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCheck : MonoBehaviour
{

    private player_movement player;
    RaycastHit2D hit;
    // Use this for initialization
    void Start()
    {
        player = transform.parent.GetComponent<player_movement>();
    }

    void Update()
    {
        if (!player.OutofControl) { checkClimb(); }
        else { player.GSanimator.enabled = false; }
    }
    void setClimbingFalse()
    {
        player.GSanimator.SetBool("isClimbing", false);
        player.GSanimator.enabled = true;
        player.GSrigid.constraints = RigidbodyConstraints2D.None;
        player.GSrigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void checkClimb()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            hit = Physics2D.Raycast(player.transform.position, Vector2.up, 0.015f, 1 << 8);

            if (hit&&!player.GSanimator.GetBool("isHurt"))
            {
                if (!player.GSanimator.GetBool("isClimbing"))
                {
                    player.GSrigid.constraints = RigidbodyConstraints2D.FreezeAll;
                    player.GSanimator.SetBool("isClimbing", true);
                }
                player.GSanimator.enabled = true;
                player.transform.Translate(0, 0.008f, 0);
            }
            else
            {
                if (player.GSanimator.GetBool("isClimbing"))
                {
                    setClimbingFalse();
                    player.GSrigid.velocity = Vector3.zero;
                    player.GSrigid.AddForce(new Vector2(0,1.5f), ForceMode2D.Impulse);
                }
               
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {

            hit = Physics2D.Raycast(player.transform.position, Vector2.down, 0.01f, 1 << 8);
            RaycastHit2D hit2 = Physics2D.Raycast(player.transform.position, Vector2.down, 0.17f, 1 << 9);

            if (hit&& !player.GSanimator.GetBool("isHurt"))
            {
                if (!hit2)
                {
                    if (!player.GSanimator.GetBool("isClimbing"))
                    {
                        player.GSrigid.constraints = RigidbodyConstraints2D.FreezeAll;
                        player.GSanimator.SetBool("isClimbing", true);
                    }
                    player.GSanimator.enabled = true;
                    player.transform.Translate(0, -0.008f, 0);
                }
                else
                {
                    setClimbingFalse();
                }
            }
            else
            {
                setClimbingFalse();
            }
        }
        else
        {
            if (!player.GSanimator.GetBool("isClimbing"))
            {
                setClimbingFalse();
            }
            else
            {
                player.GSanimator.enabled = false;
            }
        }
    }
}
