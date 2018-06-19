using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {


    private Animator animator;
    private Rigidbody2D myBody;
    private float speed = 3f;
    private bool blockTouched;
    public Transform bottomCollision;
    public LayerMask player;
    public Object flower;
    private Rigidbody2D flowerBody;
    public AudioClip sound;
    private AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        blockTouched = false;
	}
	
	// Update is called once per frame
	void Update () {
  
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!blockTouched)
        {
            if (Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f, player))
            {
                animator.SetBool("PlayerTouched", true);
                blockTouched = true;
                MoveBlock();
            }
        }

    }

    void MoveBlock()
    {
        audioSource.clip = sound;
        audioSource.Play();
        myBody.velocity = new Vector2(myBody.velocity.x, speed);
        StartCoroutine(MoveBlockTimer());

    }


    void InstantiateFlower(){
        if (gameObject.tag == MyTags.FLOWERBLOCK_TAG)
        {
            Instantiate(flower, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            audioSource.clip = sound;
            audioSource.Play();
            flowerBody = GameObject.FindWithTag(MyTags.FLOWER_TAG).GetComponent<Rigidbody2D>();
            flowerBody.velocity = new Vector2(flowerBody.velocity.x, 2f);
            StartCoroutine(StopFlower());
        }
    }
    IEnumerator MoveBlockTimer(){
        yield return new WaitForSecondsRealtime(0.1f);
        myBody.velocity = new Vector2(myBody.velocity.x, -speed);
        StartCoroutine(StopBlock());
    }
    IEnumerator StopBlock(){
        yield return new WaitForSecondsRealtime(0.1f);
        myBody.velocity = new Vector2(0, 0);
        InstantiateFlower();

    }
    IEnumerator StopFlower(){
        yield return new WaitForSecondsRealtime(0.45f);
        flowerBody.velocity = new Vector2(0, 0);
    }



}
