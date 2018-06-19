using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour {

    private Rigidbody2D myBody;
    private Animator animator;
    public float speed = 1f;
    private bool moveLeft;
    private bool IEnumeratorCalled;
    public LayerMask player;
    public Transform ray;
    private bool FlyWithoutBall;
    public GameObject ball;
    private bool playerDetected;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        moveLeft = true;
        IEnumeratorCalled = true;
        FlyWithoutBall = false;
        playerDetected = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        BirdFly();
        DetectPlayer();
	}

    void BirdFly(){
        if(moveLeft){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        }
        else if(!moveLeft){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
        if(IEnumeratorCalled){
            IEnumeratorCalled = false;
            StartCoroutine(ChangeDirection());
        }
    }

    IEnumerator ChangeDirection(){
        yield return new WaitForSeconds(5f);
        moveLeft = !moveLeft;
        ChangeScale();
        IEnumeratorCalled = true;
    }

    void ChangeScale(){
        Vector2 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
    }

    void DetectPlayer(){
        if (!playerDetected)
        {
            if (Physics2D.Raycast(ray.position, Vector2.down, 10f, player))
            {
                FlyWithoutBall = true;
                animator.SetBool("FlyWithoutBall", FlyWithoutBall);
                Instantiate(ball, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                playerDetected = true;
            }
        }
    }
}
