﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonuscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Destroy(this.gameObject);
            Debug.Log("bate");
        }
    }

}
