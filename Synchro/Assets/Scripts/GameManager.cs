using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Gamepad gamepad;

    private bool pauseFlag = false;
    private bool goalFlag = false;

    private int _playerNumber = 2;

    [SerializeField]
    private Player _player1;
    [SerializeField]
    private Player _player2;
    [SerializeField]
    private Child child;
    [SerializeField]
    private RectTransform turn;

    [SerializeField]
    private GameObject _clear;
    [SerializeField]
    private GameObject _pause;

    [SerializeField]
    private AudioClip _failSound;
    [SerializeField]
    private AudioClip _clearSound;

    private void Start()
    {
        gamepad = GameController.Instance.gamepad;
    }

    private void Update() {
        // 操作キャラクターの変更
        if (_player1.canChange && _player2.canChange)
        {
            if (gamepad.buttonB.trigger)
            {
                _player1.isControll = true;
                _player2.isControll = false;
                _playerNumber = 0;
            }
            else if (gamepad.buttonX.trigger)
            {
                _player1.isControll = false;
                _player2.isControll = true;
                _playerNumber = 1;
            }
            else if (gamepad.buttonY.trigger)
            {
                _player1.isControll = true;
                _player2.isControll = true;
                _playerNumber = 2;
            }
        }

        // 選択されている色が表示されるように回転
        turn.rotation = Quaternion.RotateTowards(turn.rotation, Quaternion.Euler(0F, 0F, _playerNumber * 120F), 180F * Time.deltaTime);

        // ゴールしていたら「ゴール」のGUIを表示
        if (goalFlag)
        {
            Pause();
            _clear.SetActive(goalFlag);

            //藤井修正
            //if (gamepad.buttonA.trigger)
            //{
            //    if (GameController.Instance.mapLevel < 15)
            //    {
            //        GameController.Instance.mapLevel++;
            //    }
            //}
        }
        else
        {
            // 「ポーズ」のGUIを表示
            if (gamepad.startButton.trigger)
            {
                Pause();
                _pause.SetActive(!_pause.activeSelf);
            }
        }
    }

    private void Clear()
    {
        goalFlag = true;
        SoundManager.instance.PlaySFX(_clearSound);
    }

    private void Fail()
    {
        SoundManager.instance.PlaySFX(_failSound);
        _player1.Restart();
        _player2.Restart();
        child.Restart();
    }

    private void Pause() {
        // 移動等の処理を停止
        _player1.enabled = _pause.activeSelf;
        _player2.enabled = _pause.activeSelf;
        child.enabled = _pause.activeSelf;
        // 物理演算の処理を停止
        _player1.GetComponent<Rigidbody>().isKinematic = !_pause.activeSelf;
        _player2.GetComponent<Rigidbody>().isKinematic = !_pause.activeSelf;
        child.GetComponent<Rigidbody>().isKinematic = !_pause.activeSelf;
    }
}
