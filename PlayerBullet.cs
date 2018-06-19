using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    private float bulletSpeed = 8f;
    private Rigidbody2D myBody;
    private Animator animator;
    private bool bulletFired;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(PlayerMovement.playerDirection == 0){
            bulletSpeed *= -1;
        }
        bulletFired = false;
        StartCoroutine(DeactivateBullet_IFEnemyNotFound());
    }
    // Update is called once per frame
    void FixedUpdate () {
        MoveBullet();
	}

    void MoveBullet(){
        myBody.velocity = new Vector2(bulletSpeed, myBody.velocity.y);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != MyTags.PLAYER_TAG)
        {
            bulletFired = true;
            animator.SetBool("BulletFired", bulletFired);
        }
    }

    void DeactivateBullet(){
        gameObject.SetActive(false);
    }

    IEnumerator DeactivateBullet_IFEnemyNotFound(){
        yield return new WaitForSeconds(2f);
        print("up");
        gameObject.SetActive(false);
    }

}
