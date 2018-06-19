using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {


    public Text coinsText;
    public Text lifeText;
    public static int lifes;
    public static int coins;
	// Use this for initialization
	void Start () {
        lifes = 3;
        coins = 0;
	}
	
	// Update is called once per frame
	void Update () {
        coinsText.text = "x" + coins;
        lifeText.text = "x" + lifes;
	}

    void QuitGame(){
        if(lifes < 0){
            CameraAnimation.screenRed = true;
            StartCoroutine(QuitAnimation());
        }
    }
    IEnumerator QuitAnimation(){
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
