using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameRota : MonoBehaviour {

    [SerializeField]
    private bool left = true;

    [SerializeField]
    private float speed ;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //右周りか左周りか
        if (left)
        {
            //回す
            //transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
        }
        else
        {
            //回す
            //transform.Rotate(new Vector3(0, 0, -speed) * Time.deltaTime);
        }
    }
}
