using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour {

    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float velocity = myBody.velocity.y;
        if(collision.gameObject.tag == MyTags.PLAYER_TAG && velocity > -1f && velocity < 1f){
            gameObject.SetActive(false);
        }
    }
}
