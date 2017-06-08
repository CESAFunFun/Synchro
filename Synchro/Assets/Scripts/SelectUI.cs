using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] _child;

    [SerializeField]
    float _speed;

    bool _rotFlag;

    public bool RotFlag { set { _rotFlag = value; } }

	// Use this for initialization
	void Start () {
        _rotFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!_rotFlag) return;

        var flag = false;
        for(var i = 0;i < _child.Length; i++)
        {
            if (i % 2 == 0)
                flag = !flag;

            if(flag)
                _child[i].transform.Rotate(new Vector3(0, 0, _speed) * Time.deltaTime);
            else
                _child[i].transform.Rotate(new Vector3(0, 0, -_speed) * Time.deltaTime);
        }
	}
}
