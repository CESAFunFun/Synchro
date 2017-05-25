using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour {

    [SerializeField]
    private Gamepad _gamepad;
    [SerializeField]
    private float _roteSpeed = 60F;
    [SerializeField]
    private Camera _mainCamera;
    [SerializeField]
    private Camera _subCamera;

    private float _angle;
    private int _side;

	// Use this for initialization
	void Start () {
        _angle = 0F;
        _side = 0;
	}

    // Update is called once per frame
    void Update() {
        if(_gamepad.rightStickPress.trigger)
        {
            // アクティブとなっていない方のカメラに切り替える
            ChangeDisplay(!_mainCamera.gameObject.activeSelf);
        }

        // カメラの角度を変える
        RotationCamera();
    }

    private void ChangeDisplay(bool main)
    {
        // どちらかのアクティブが有効になる
        _mainCamera.gameObject.SetActive(main);
        _subCamera.gameObject.SetActive(!main);
        // ここで正面に角度を修正させる
        transform.rotation = Quaternion.identity;
    }

    private void RotationCamera()
    {
        //回転している最中かの真偽が返ってくる
        // 左への回転処理
        if (RotaTarget()) return;
        

        //入力処理
        //回転してなかったら処理
        if (_gamepad.rightStick.x <= -1F)
        {
            _side = +1;
        }
        if (_gamepad.rightStick.x >= 1F)
        {
            _side = -1;
        }
    }

    bool RotaTarget()
    {
        // 回転中かどうかを判定
        if (_side != 0 && _angle < 45F)
        {
            _angle += Time.deltaTime * _roteSpeed;
            transform.Rotate((Vector3.up * _side) * Time.deltaTime * _roteSpeed);
            return true;
        }

        // 回転していない状態に戻す
        _angle = 0F;
        _side = 0;
        return false;
    }
}
