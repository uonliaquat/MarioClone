using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour {
    

    private Rigidbody2D myBody;
    private Animator animator;
    public float speed = 5f;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    private float jumpPower = 7.4f;
    private bool isGrounded;
    public GameObject bullet;
    public static int playerDirection;
    public static bool stealthMode;
    public static float h;
    public AudioClip jumpSound, coinSound, bulletSound;
    private AudioSource audioSource;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        
        isGrounded = true;
        playerDirection = 0;
        stealthMode = false;
	}

    private void Update()
    {

    }
    void FixedUpdate () {
        PlayerWalk();
        PlayerJump();
        FireBullet();
	}

    void PlayerWalk(){
        h = Input.GetAxisRaw("Horizontal");
        if(h > 0){
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
            playerDirection = 1;
        }
        else if(h < 0){
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
            playerDirection = 0;
        }
        else {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
        }

        animator.SetInteger("Speed",(int) Mathf.Abs((int) myBody.velocity.x));
    }

    void PlayerJump(){
        if (isGrounded){
            if (Input.GetKeyDown(KeyCode.Space)){
                audioSource.clip = jumpSound;
                audioSource.Play();
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                animator.SetBool("Jump", true);
                isGrounded = false;
            }
        }
    }

    void ChangeDirection(int direction){
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
            animator.SetBool("Jump", false);
            isGrounded = true;
    }

    void FireBullet(){
        if(Input.GetKeyDown(KeyCode.J)){
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            audioSource.clip = bulletSound;
            audioSource.Play();

      
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float velocity  = 0;
        if (collision.gameObject.tag == MyTags.FLOWER_TAG)
        {
            velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
        }
        if(collision.gameObject.tag == MyTags.FLOWER_TAG && collision.gameObject.GetComponent<Rigidbody2D>() && velocity > -1f && velocity < 1f){
            stealthMode = true;
            animator.SetBool("Blink", stealthMode);
            StartCoroutine(DisableStealthMode());
        }
        if(collision.gameObject.tag == MyTags.COIN_TAG){
            audioSource.clip = coinSound;
            audioSource.Play();
        }
        if(collision.gameObject.tag == MyTags.WATER_TAG){
            StartCoroutine(RestartGame());
        }
    }


    IEnumerator DisableStealthMode(){
        yield return new WaitForSeconds(10f);
        stealthMode = false;
        animator.SetBool("Blink", stealthMode);
    }

    IEnumerator RestartGame(){
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }

}
