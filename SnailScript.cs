using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour {

    private Rigidbody2D myBody;
    private float speed = 0.5f;
    public Transform snailCheckPosition,snailCheckPositionTop, snailCheckPositionLeft, snailCheckPositionRight;
    private bool moveLeft;
    public LayerMask player;
    private Animator animator;
    private bool stunned;
    private bool snailTimer = false;
    private bool objectKilled;
    private BoxCollider2D boxCollider2d;
    private bool stealthMode;
    public LayerMask wood;
    private Transform temp;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }
    // Use this for initialization
    void Start () {
        moveLeft = true;
        stunned = false;
        snailTimer = true;
        objectKilled = false;
        stealthMode = false;

	}
    private void Update()
    {
        //CheckStealthMode();
    }
    private void FixedUpdate()
    {
        if(!objectKilled){
            SnailWalk();
            CheckStunned();
        }
    }

    void SnailWalk(){
        if (!stunned)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(speed, myBody.velocity.y);
            }

            //if (gameObject.tag == MyTags.BEETLE_TAG){
            //    StartCoroutine(ChangeBeetlePosition());
            //}
            CheckCollision();
        }
    }

    void CheckCollision(){
        if(gameObject.tag == MyTags.SNAIL_TAG){
            if (!Physics2D.Raycast(snailCheckPosition.position, Vector2.down, 0.1f))
            {
                moveLeft = !moveLeft;
                ChangeScale();
            }
        }
        else if(gameObject.tag == MyTags.BEETLE_TAG){
            if (snailTimer)
            {
                snailTimer = false;
                StartCoroutine(ChangeBeetlePosition());
            }
        }
 
    }
    void ChangeScale(){
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    void CheckStunned(){
        Collider2D col = Physics2D.OverlapCircle(snailCheckPositionTop.position, 0.15f, player);
        if (!stunned)
        {
            if (col != null)
            {
                stunned = true;
                animator.SetBool("Stunned", stunned);
                myBody.velocity = new Vector2(0, 0);
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 5f);
            }
        }
        if (stunned)
        {
  
            if (Physics2D.Raycast(snailCheckPositionLeft.position, Vector2.left, 0.1f, player))
            {
                RaycastHit2D leftHit = Physics2D.Raycast(snailCheckPositionLeft.position, Vector2.left, 0.1f, player);
                if (leftHit.collider.tag == MyTags.PLAYER_TAG)
                {
                    MoveSnailStunned();
                }
                else if (gameObject.tag == MyTags.BEETLE_TAG)
                {
                    StartCoroutine(DeactivateSnail());
                }
            }
            else if(Physics2D.Raycast(snailCheckPositionRight.position, Vector2.right, 0.1f, player)){
                RaycastHit2D rightHit = Physics2D.Raycast(snailCheckPositionRight.position, Vector2.right, 0.1f, player);
                if (rightHit.collider.tag == MyTags.PLAYER_TAG)
                {
                    MoveSnailStunned();
                }
                //else if (col.gameObject.tag == MyTags.BEETLE_TAG)
                //{
                //    StartCoroutine(DeactivateSnail());
                //}
            }
            else if(Physics2D.Raycast(snailCheckPositionTop.position, Vector2.up, 0.1f, player)){
                print("move");
                if (gameObject.tag == MyTags.BEETLE_TAG)
                {
                    StartCoroutine(DeactivateSnail());
                }
            }
          
        }       
    }
    void MoveSnailStunned()
    {
      
        if (Physics2D.Raycast(snailCheckPositionLeft.position, Vector2.left, 0.1f, player))
        {
            myBody.velocity = new Vector2(20f, myBody.velocity.y);
            StartCoroutine(DeactivateSnail());
        }
        else if (Physics2D.Raycast(snailCheckPositionRight.position, Vector2.right, 0.1f, player))
        {
            myBody.velocity = new Vector2(-20f, myBody.velocity.y);
            StartCoroutine(DeactivateSnail());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.BULLET_TAG){
            StartCoroutine(DeactivateSnail());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.PLAYER_TAG && !stunned){
            if (!stealthMode)
            {
                UIScript.lifes--;
                CameraAnimation.screenRed = true;
            }
        }
        if(collision.gameObject.tag == MyTags.WOOD_TAG || collision.gameObject.tag == MyTags.STAIR_TAG){
            if(!stunned){
                moveLeft = !moveLeft;
                ChangeScale();
            }
        }
        if (collision.gameObject.tag == MyTags.BEETLE_TAG)
        {
            if (!stunned)
            {
                moveLeft = !moveLeft;
                ChangeScale();
            }
        }
    }

    void CheckStealthMode(){
        if (PlayerMovement.stealthMode)
        {
            stealthMode = true;
            boxCollider2d.isTrigger = stealthMode;
        }
        else if (!PlayerMovement.stealthMode){
            stealthMode = false;
            boxCollider2d.isTrigger = stealthMode;
        }
    }

    IEnumerator DeactivateSnail(){
        objectKilled = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    IEnumerator ChangeBeetlePosition(){
        yield return new WaitForSeconds(5f);
        moveLeft = !moveLeft;
        ChangeScale();
        snailTimer = true;
    }

}
