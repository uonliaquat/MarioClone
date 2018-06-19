using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{

    public LayerMask ground;
    public Transform ray;
    private float speed = -0.01f;
    private Animator animator;
    private Rigidbody2D myBody;
    private bool objectKilled;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
        objectKilled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!objectKilled){
            Crawl();
        }
    }

    void Crawl()
    {
        Vector2 tempPosition = transform.position;
        tempPosition.y += speed;
        transform.position = tempPosition;
        DetectGround();
    }

    void DetectGround()
    {
        if (Physics2D.Raycast(ray.position, Vector2.down, 1f, ground))
        {
            ChangeScale();
            StartCoroutine(ComeDown());
        }
    }

    void ChangeScale()
    {
        Vector2 tempSacle = transform.localScale;
        tempSacle.y *= -1;
        transform.localScale = tempSacle;
        speed *= -1;
    }

    IEnumerator ComeDown(){
        yield return new WaitForSeconds(4f);
        ChangeScale();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.PLAYER_TAG){
            //Damage Player
            CameraAnimation.screenRed = true;
            UIScript.lifes--;
        }
        else if(collision.gameObject.tag == MyTags.BULLET_TAG){
            objectKilled = true;
            StartCoroutine(DeactivateObject());
        }
    }
    IEnumerator DeactivateObject(){
        animator.SetBool("SpiderKilled",true);
        myBody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
