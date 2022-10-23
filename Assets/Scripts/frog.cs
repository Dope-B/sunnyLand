using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : MonoBehaviour {

    Rigidbody2D rigid;
    public Rigidbody2D GS_rigid { get { return rigid; } set { rigid = GS_rigid; } }
    public int state = 1;
    Animator animator;
    RaycastHit2D hit;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine("stateChecker");
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "player_body")
        {
            PlayerStat.instance.Hit(15);
            player_movement.player.damaged();
        }
    }
    // Update is called once per frame
    void FixedUpdate() {
        checkFalling();
    }
    IEnumerator stateChecker()
    {
        state = Random.Range(0, 2);
        switch (state)
        {
            case 0:
                transform.localScale = new Vector3(-1, 1, 1);
                rigid.AddForce(new Vector2(1f, 2f), ForceMode2D.Impulse);
                break;
            case 1:
                transform.localScale = new Vector3(1, 1, 1);
                rigid.AddForce(new Vector2(-1f, 2f), ForceMode2D.Impulse);
                break;
        }
    
    yield return new WaitForSeconds(5f);
        StartCoroutine("stateChecker");
    }
    void jump() {

        switch (state) {
            case 0:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case 1:
                transform.localScale = new Vector3(1, 1, 1);
                break;
        }
    }
    void checkFalling()
    {
        if (rigid.velocity.y < -0.01) { animator.SetBool("isFalling", true); }
        else if (rigid.velocity.y > 0.01)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", true);
        }
        else if (rigid.velocity.y == 0)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
}
