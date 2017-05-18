using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour {

    private Gamepad _gamepad;

	// Use this for initialization
	void Start () {
        _gamepad = GetComponent<Gamepad>();
	}

    // Update is called once per frame
    void Update() {
        if (_gamepad.rightStick != Vector2.zero)
        {
            transform.Rotate(Vector3.down, _gamepad.rightStick.x);
            transform.Rotate(Vector3.left, _gamepad.rightStick.y);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
}
