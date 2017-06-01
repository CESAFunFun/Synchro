using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public Vector2 maximum;
    public Vector2 minimum;

    private Gamepad _gamepad;

    private Vector3 _startPosition;

    [SerializeField]
    private float speed=5;

    // Use this for initialization
    void Start () {
        _gamepad = GameManager.Instance.gamePad;
        _startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if(_gamepad==null)
            _gamepad = _gamepad = GameManager.Instance.gamePad;


        Vector3 move = Vector3.zero;
        //入力処理
        //回転してなかったら処理(X軸)
        if (_gamepad.rightStick.x <= -1F)
        {
            //左端までしか移動できないようにする
            if (transform.position.x > minimum.x)
            {
                move.x = -speed;
                transform.Translate(move * Time.deltaTime);
            }
        }
        if (_gamepad.rightStick.x >= 1F)
        {
            if (transform.position.x < maximum.x)
            {
                move.x = speed;
                transform.Translate(move * Time.deltaTime);
            }
        }

        //入力処理
        //回転してなかったら処理(Y軸)
        if (_gamepad.rightStick.y <= -1F)
        {
            if (transform.position.y < maximum.y)
            {
                move.y = speed;
                transform.Translate(move * Time.deltaTime);
            }
        }
        if (_gamepad.rightStick.y >= 1F)
        {
            if (transform.position.y > minimum.y)
            {
                move.y = -speed;
                transform.Translate(move * Time.deltaTime);
            }
        }

        if(_gamepad.rightStickPress.trigger)
        {
            transform.position = _startPosition;
        }
    }
}
