using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag == MyTags.PLAYER_TAG){
            //Damage Player
            CameraAnimation.screenRed = true;
        }
        gameObject.SetActive(false);
    }
}
