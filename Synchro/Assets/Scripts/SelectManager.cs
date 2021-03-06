﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectManager : MonoBehaviour
{

    [SerializeField]
    private SelectUI[] _stagenum;

    private Gamepad _gamepad;

    [SerializeField]
    private int _number = 0;

    private int _postnum;

    [SerializeField]
    float _waitTime;

    float _curremtTime;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform[] _cameraPos;

    private Vector3 _lastPos;

    [SerializeField]
    private GameObject[] _nextArrow;

    [SerializeField]
    private AudioClip _cursorSound;

    // Use this for initialization
    void Start()
    {
        // 静的なメンバからゲームパッドを取得
        _gamepad = GameController.Instance.gamepad;
        //XXX:Maplevelが0の時にエラーが出ます
        _number = GameController.Instance.mapLevel - 1;
        _stagenum[_number].RotFlag = true;
    }
    // Update is called once per frame
    void Update()
    {
        // 入力待ち用の時間を更新
        _curremtTime += Time.deltaTime;

        //カメラの前の座標
        _lastPos = _camera.transform.position;

        // 分割された画面の正面になるように位置の線形補間を行う
        _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPos[_number / 5].position, 15F * Time.deltaTime);

        //カメラが移動してたら以下の処理をしない
        if (_lastPos != _camera.transform.position)
        {
            for (int i = 0; i < _nextArrow.Length; i++)
                _nextArrow[i].SetActive(false);
            return;
        }
        else
        {
            for (int i = 0; i < _nextArrow.Length; i++)
                _nextArrow[i].SetActive(true);
        }

        // 待機時間を超えていれば以下の処理をしない
        if (_curremtTime < _waitTime) return;

        // コントローラの入力で移動
        int num = 0;
        if (_gamepad.leftStick.x <= -1F)
        {
            num += -1;
        }
        else if (_gamepad.leftStick.x >= 1F)
        {
            num += +1;
        }
        else
        {
            _gamepad.leftStick.x = 0;
        }

        // 回転するカーソルを選択
        if (num != 0)
        {
            _stagenum[_number].RotFlag = false;
            _postnum = _number;
            _number += num;
            _number = Mathf.Clamp(_number, 0, _stagenum.Length - 1);
            GameController.Instance.mapLevel = _number + 1;
            _stagenum[_number].RotFlag = true;
            _curremtTime = 0;
            if(_postnum!=_number)
                SoundManager.instance.PlaySFX(_cursorSound);
        }

        // プレイシーンへ移行
        if (_gamepad.buttonA.trigger)
        {
            GameController.Instance.mapLevel = _number + 1;
            SceneManager.LoadScene("Play");
        }

        if(_gamepad.buttonB.trigger)
        {
            SceneManager.LoadScene("Title");
        }

        //if (_number <= 4 && _number >= 0)
        //{
        //    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[0].position, 1.0f);
        //}
        //else if (_number <= 9 && _number >= 5)
        //{
        //    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[1].position, 1.0f);
        //}
        //else
        //{
        //    _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[2].position, 1.0f);
        //}
    }
}
