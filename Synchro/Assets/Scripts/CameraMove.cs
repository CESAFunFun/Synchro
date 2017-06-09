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

    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _subCamera;

    // Use this for initialization
    void Start () {
        _gamepad = GameController.Instance.gamepad;
        _startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (_gamepad.rightStickPress.trigger)
        {
            // アクティブとなっていない方のカメラに切り替える
            ChangeDisplay(!_mainCamera.gameObject.activeSelf);
        }

        Vector3 move = Vector3.zero;
        //入力処理
        //回転してなかったら処理(X軸)
        if (_gamepad.rightStick.x <= -1F)
        {
            //左端までしか移動できないようにする
            if (transform.position.x > minimum.x)
            {
                if (_mainCamera.gameObject.activeSelf)
                    move.x = -speed;
                else
                    move.x = speed;
                transform.Translate(move * Time.deltaTime);
            }
        }
        if (_gamepad.rightStick.x >= 1F)
        {
            if (transform.position.x < maximum.x)
            {
                if (_mainCamera.gameObject.activeSelf)
                    move.x = speed;
                else
                    move.x = -speed;
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
    private void ChangeDisplay(bool main)
    {
        // どちらかのアクティブが有効になる
        _mainCamera.gameObject.SetActive(main);
        _subCamera.gameObject.SetActive(!main);
        // ここで正面に角度を修正させる
        transform.rotation = Quaternion.identity;
    }
}
