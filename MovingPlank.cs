using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlank : MonoBehaviour {

    public static float speed;
    private Rigidbody2D myBody;
    private bool movingRight;
    private bool ienumeratorCalled;
    private float time;
    public GameObject player;
    public static bool playerCollided;
    // Use this for initialization

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    void Start () {
        movingRight = true;
        ienumeratorCalled = false;
        playerCollided = false;
        if(gameObject.tag == MyTags.MOVINGPLATFORM1_TAG){
            speed = 2f;
            time = 3f;
        }
        else if (gameObject.tag == MyTags.MOVINGPLATFORM2_TAG){
            speed = 5.5f;
            time = 3f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        MovePlank();
        if(playerCollided){
            float h  = Input.GetAxisRaw("Horizontal");
            if(h > 0){
                Vector3 tempPosition = player.transform.position;
                tempPosition.x += 0.04f;
                player.transform.position = tempPosition;
            }
            else if(h < 0){
                Vector3 tempPosition = player.transform.position;
                tempPosition.x -= 0.04f;
                player.transform.position = tempPosition;
            }

        }
	}

    private void FixedUpdate()
    {
        if(playerCollided){
            if (movingRight)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, player.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
            else if(!movingRight){
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, player.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }

    void MovePlank(){
        if(movingRight){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
        }
        else if(!movingRight){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        }
        if(!ienumeratorCalled){
            StartCoroutine(changeDirection(time));
            ienumeratorCalled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.PLAYER_TAG){
            playerCollided = true;

        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerCollided = false;
    }

    IEnumerator changeDirection(float time){
        yield return new WaitForSeconds(time);
        movingRight = !movingRight;
        ienumeratorCalled = false;

    }

}
