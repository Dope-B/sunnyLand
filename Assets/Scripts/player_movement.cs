using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

    static public player_movement player;
    private PlayerStat stat;
    public enemy_kill GSbox { get { return box; } set { box = GSbox; } }
    public CapsuleCollider2D GScap { get { return cap; } set { cap = GScap; } }
    public Rigidbody2D GSrigid { get { return rigid; } set { rigid = GSrigid; } }
    public Animator GSanimator { get { return animator; } set { animator = GSanimator; } }
    private enemy_kill box;
    RaycastHit2D hit;
    private CapsuleCollider2D cap;
    public BoxCollider2D mapBound;
    private Rigidbody2D rigid;
    private GameObject Ladder;
    private Animator animator;
    private Vector3 moveVelocity;
    private Vector3 limitPos;
    private AudioManager Audio;
    public string CurrentMap;
    public string PreviousMap;
    private bool isJump = false;
    public bool isInc = false;
    private int jump_count = 2;
    private int inc_Timer = 0;
    public bool isTrans = false;
    public bool ischoice = false;
    public bool OutofControl=false;
    private bool canMove = true;
    private bool canJump = true;
    public bool istalking = false;
    public float speed = 1.3f;
    public float jumpPower = 2.2f;
    public int inc_Delay = 40;
    public int hurtDelay = 20;
    public string jumpSound;
    public string hurtSound;

    // Use this for initialization
    void Start () {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(this.gameObject);
            rigid = gameObject.GetComponent<Rigidbody2D>();
            animator = gameObject.GetComponent<Animator>();
            cap = gameObject.GetComponent<CapsuleCollider2D>();
            box = transform.GetComponentInChildren<enemy_kill>();
            mapBound = FindObjectOfType<cam_manager>().bound;
            Audio = FindObjectOfType<AudioManager>();
        }
        else { Destroy(this.gameObject); }
       
    }
    void Update()
    {
        if (!OutofControl)
        {
            if (canMove && !istalking) { Move(); }
            if (canJump && !istalking) { Jump(); }
            climbJump();
        }
    }
    void FixedUpdate() {
        
            checkFalling();
            hurtdelayCheck();
            checkState();
            LimitPosition();
            setUIstate();
    }
    void Move() {
            moveVelocity = Vector3.zero;
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                hit = Physics2D.Raycast(player.transform.position, Vector2.left, 0.085f, 1 << 9);
                if (hit)
                {
                    moveVelocity = Vector3.zero;
                }
                else
                {
                    moveVelocity = Vector3.left;
                }
                transform.localScale = new Vector3(-1, 1, 1);
                animator.SetBool("isMoving", true);

            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                hit = Physics2D.Raycast(player.transform.position, Vector2.right, 0.085f, 1 << 9);
                if (hit)
                {
                    moveVelocity = Vector3.zero;
                }
                else
                {
                    moveVelocity = Vector3.right;
                }
                transform.localScale = new Vector3(1, 1, 1);
                animator.SetBool("isMoving", true);
            }
            transform.position += moveVelocity * speed * Time.deltaTime;     
    }
    void Jump() {
            if (Input.GetButtonDown("Jump") && jump_count != 0 && !isJump)
            {
                isJump = true;
                rigid.velocity = Vector2.zero;
                Audio.play(jumpSound,1f);
                Vector2 jumpVelocity = new Vector2(0, jumpPower);
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                --jump_count;
            }
            else { isJump = false; }     
    }
    void climbJump() {
        if((Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow))&& Input.GetButtonDown("Jump") && animator.GetBool("isClimbing"))
        {
            player.GSanimator.enabled = true;
            rigid.constraints = RigidbodyConstraints2D.None;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("isClimbing", false);
            Move();
            Jump();
        }
    }
    void checkFalling() {
        if (!player.animator.GetBool("isHurt")) { 
            if (rigid.velocity.y < -0.01) { animator.SetBool("isFalling", true); if (jump_count == 2) { jump_count = 1; } }
            else if (rigid.velocity.y > 0.01)
            {
                animator.SetTrigger("doJumping");
                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", true);
            }
            else if (rigid.velocity.y == 0)
            {
                if (!animator.GetBool("isFalling") && !animator.GetBool("isJumping"))
                {
                    jump_count = 2;
                    isJump = false;
                }
                
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
    void checkState() {
        if (animator.GetBool("isHurt")||animator.GetBool("isClimbing"))
        {
            canMove = false;
            canJump = false;
            box.GSbox.enabled = false;
            if (animator.GetBool("isHurt"))
            {
                animator.SetBool("isClimbing", false);
                animator.enabled = true;
            }
        }
        else {
            canJump = true;
            canMove = true;
            box.GSbox.enabled = true;
        }
        if(Input.GetAxisRaw("Horizontal") == 0) { animator.SetBool("isMoving", false); }
        
    }
    void hurtdelayCheck() {
        if (isInc&&!OutofControl)
        {
            inc_Timer++;
            if (inc_Timer == hurtDelay) { animator.SetBool("isHurt", false);}
            if (inc_Timer == inc_Delay) { isInc = false;inc_Timer = 0; }
        }
    }
    void LimitPosition()
    {
        float clampX = Mathf.Clamp(this.transform.position.x, mapBound.bounds.min.x+0.08f, mapBound.bounds.max.x-0.08f);
        float clampY = Mathf.Clamp(this.transform.position.y, mapBound.bounds.min.y, mapBound.bounds.max.y);
        transform.position = new Vector3(clampX, clampY, this.transform.position.z);
    }
    void bounce()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(new Vector2(-Input.GetAxisRaw("Horizontal")*0.5f, 1.5f), ForceMode2D.Impulse);
    }
    private void setUIstate()
    {
        if (ischoice) { OutofControl = true; }
        if (OutofControl) { isInc = true; animator.SetBool("isMoving", false); }
    }
    public void damaged()
    {
        if (!isInc)
        {
            Audio.play(hurtSound,1f);
            bounce();
            animator.SetBool("isHurt", true);
            isInc = true;
        }
    }
}
