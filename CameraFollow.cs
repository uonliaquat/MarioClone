using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    public GameObject player;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 tempPosition = transform.position;
        tempPosition.x = player.transform.position.x + 1f;
        tempPosition.z = -10;
        tempPosition.y = 0;
        transform.position = tempPosition;
        //transform.position = player.transform.position + offset;
	}
}
