using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {

    public bool[] evaluation;

    [SerializeField]
    private GameObject[] _sprite; 

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
		
        for(int i=0;i<evaluation.Length;i++)
        {
            if(evaluation[i])
            {
                _sprite[i].active = true;
            }
            else
            {
                _sprite[i].active = false;
            }
        }

	}

}
