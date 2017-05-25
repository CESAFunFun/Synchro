using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    [SerializeField]
    Transform pos1;

    [SerializeField]
    Transform pos2;

    LineRenderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Player.conectFlag)
            renderer.enabled = true;
        else
            renderer.enabled = false;

        renderer.SetPosition(0, pos1.position);
        renderer.SetPosition(1, pos2.position);
    }
}
