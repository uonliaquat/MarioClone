using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour {

    private Rigidbody2D myBody;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
        myBody.AddForce(new Vector2(Random.Range(-330, -700), 0));
        StartCoroutine(DeactivateStone());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.PLAYER_TAG){
            //Damage Player
        }
    }

    IEnumerator DeactivateStone(){
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
