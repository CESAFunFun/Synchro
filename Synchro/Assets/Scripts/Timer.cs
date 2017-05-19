using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float countTime = 0;

    public int seconds = 0;

    public int minute = 0;

    public bool stop = false;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {

        if (!stop)
        {
            countTime += Time.deltaTime;

            if (seconds <= 60)
                if (countTime >= 1.0f)
                {
                    seconds++;
                    countTime = 0;
                }
            if (minute <= 98)
                if (seconds >= 60.0f)
                {
                    minute++;
                    seconds = 0;
                }
        }
	}
}
