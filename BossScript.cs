using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    private Animator animator;
    private bool attacking;
    public GameObject stone;
    public Transform instantiatePosition;
    private int bossHealth;
    private bool bossKilled;
    public Transform detectPlayer;
    public LayerMask player;
    public AudioClip bossShot;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        attacking = true;
        bossHealth = 10;
        bossKilled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics2D.Raycast(detectPlayer.position, Vector2.left, 6f, player))
        {
            if (!bossKilled)
            {
                Attack();
                if (bossHealth == 0)
                {
                    Kill();
                }
            }
        }
	}

    void Attack(){
        if(attacking){
            animator.SetBool("Attack", attacking);
            StartCoroutine(AttackGap());
            attacking = false;
        }
    }
    void Idle(){
        attacking = false;
        animator.SetBool("Attack", attacking);
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.BULLET_TAG)
        {
            bossHealth--;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.PLAYER_TAG){
            UIScript.lifes--;
        }
    }
    void InstatniateStone(){
        Instantiate(stone, instantiatePosition.position, Quaternion.identity);
        audioSource.clip = bossShot;
        audioSource.Play();
    }

    void Kill(){
        animator.SetBool("Dead", true);
        bossKilled = true;
        StartCoroutine(DeactivateBoss());
    }
    IEnumerator DeactivateBoss(){
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    IEnumerator AttackGap(){
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        attacking = true;
    }
}
