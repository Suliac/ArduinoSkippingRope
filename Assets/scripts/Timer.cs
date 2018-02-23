using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {



    public static  float Timercount;
    private Text Timertext;
    public float timer;

	// Use this for initialization
	void Start () {

        Timercount = timer;
        Timertext = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {

        Timercount -= Time.deltaTime;

        if (Timercount >=0)
        {
            Timertext.text = Timercount.ToString("f2");

        }

        if (Timercount <= 0)
        {
            Timertext.text = "you NOOB !";

        }

    }
}
