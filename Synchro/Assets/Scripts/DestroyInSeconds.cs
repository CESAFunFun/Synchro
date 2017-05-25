﻿//---------------------------
// 秒数で自分を破棄する
//---------------------------
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour {

    private float _time;

    [SerializeField]
    private float _destroyIn = 2;
	
	// Update is called once per frame
	void Update () {
        _time++;

        //時間たつと破棄する
        if(_time >= 60 * _destroyIn)
        {
            _time = 0;
            Destroy(gameObject);
        }
	}
}
