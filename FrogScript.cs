using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour {


    private Animator animator;
    private Rigidbody2D myBody;
    private float speedX = 2f;
    private float speedY = 3f;
    private bool IEnumeratorCalled;
    public LayerMask ground;
    private int jumpCount;
    public Transform topCollision;
    public LayerMask player;
    private bool objectKilled;
    public Transform leftCollision, rightCollision;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start () {
        IEnumeratorCalled = false;
        jumpCount = 0;
        objectKilled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!objectKilled)
        {
            if (jumpCount >= 5)
            {
                ChangeScale();
                jumpCount = 0;
            }
            CheckKill();
            CheckCollion();
        }
	}
    private void FixedUpdate()
    {
        if(!objectKilled){
            Jump();
        }
    }

    void CheckCollion(){
        if(Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, player)){
            //Damage Player
            CameraAnimation.screenRed = true;
            UIScript.lifes--;
        }
        else if(Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, player)){
            CameraAnimation.screenRed = true;
            UIScript.lifes--;
        }
    }

    void Jump(){
        if (!IEnumeratorCalled)
        {
            myBody.velocity = new Vector2(-speedX, speedY);
            animator.SetBool("FrogJumped", true);
        }
        if(!IEnumeratorCalled){
            StartCoroutine(GetDown());
            IEnumeratorCalled = true;
        }
    }
    IEnumerator GetDown(){
        yield return new WaitForSeconds(4f);
        myBody.velocity = new Vector2(0, 0);
        IEnumeratorCalled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.GROUND_TAG){
            animator.SetBool("FrogJumped", false);
            jumpCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            StartCoroutine(DeactivateObject());
        }
    }
    void ChangeScale(){
        Vector2 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
        speedX *= -1;
    }

    void CheckKill(){
        if(Physics2D.OverlapCircle(topCollision.position, 0.1f, player)){
            StartCoroutine(DeactivateObject());
        }
    }
    IEnumerator DeactivateObject(){
        animator.SetBool("FrogKilled", true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        objectKilled = true;
    }

}
