using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {
    
    private Animator animator;
    public static bool screenRed;
    private bool gameStarted;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
        screenRed = false;
        gameStarted = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(screenRed){
            PlayerDamageAnimation();
        }
        else if(!screenRed){
            IdleAnimation();
        }
	}

    void IdleAnimation(){
        screenRed = false;
        animator.SetBool("ScreenRed",screenRed);
    }
    void PlayerDamageAnimation(){
        screenRed = true;
        animator.SetBool("ScreenRed", screenRed);
    }

    void GameStarted(){
        animator.SetBool("GameStarted", gameStarted);
    }
}
