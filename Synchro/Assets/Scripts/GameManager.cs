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

    private int _playerNumber = 0;
    private int _oldNumber = -1;

    private float turnCounter = 0F;
    private float recastTimer = 0.5F;

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
    [SerializeField]
    private AudioClip _pauseSound;
    [SerializeField]
    private AudioClip _trunSound;




    private void Start()
    {
        gamepad = GameController.Instance.gamepad;
    }

    private void Update() {
        // 操作するキャラクターを切り替える
        SwitchPlayerControll();

        // 選択されている色が表示されるように回転
        turn.rotation = Quaternion.RotateTowards(turn.rotation, Quaternion.Euler(0F, 0F, _playerNumber % 3 * 120F), 180F * Time.deltaTime);

        // ゴールしていたら「ゴール」のGUIを表示
        if (goalFlag)
        {
            Pause();
            _clear.SetActive(goalFlag);
        }
        else
        {
            // 「ポーズ」のGUIを表示
            if (gamepad.startButton.trigger)
            {
                SoundManager.instance.PlaySFX(_pauseSound);
                Pause();
                _pause.SetActive(!_pause.activeSelf);
               
            }
        }
    }

    private void SwitchPlayerControll() {

        // マップ上で操作不可を設定されれば以下を処理しない
        if (!_player1.canChange || !_player2.canChange) return;

        turnCounter += Time.deltaTime;
        if (turnCounter <= recastTimer) return;

        // 操作キャラクターをボタンで切り替え
        if (gamepad.buttonX.trigger)
        {
            // UIの回転音を再生
            SoundManager.instance.PlaySFX(_trunSound);
            turnCounter = 0F;

            // 操作を左回転
            if (_playerNumber < 2)
            {
                _playerNumber++;
            }
            else
            {
                _playerNumber = 0;
            }
        }

        if (gamepad.buttonY.trigger)
        {
            // UIの回転音を再生
            if(_playerNumber != 0)
            SoundManager.instance.PlaySFX(_trunSound);
            turnCounter = 0F;

            // 全てへの操作に変更
            _playerNumber = 0;
        }

        if (gamepad.buttonB.trigger)
        {
            // UIの回転音を再生
            SoundManager.instance.PlaySFX(_trunSound);
            turnCounter = 0F;

            // 操作を右回転
            if (_playerNumber > 0)
            {
                _playerNumber--;
            }
            else
            {
                _playerNumber = 2;
            }
        }

        // 操作するキャラクターを算出
        switch (_playerNumber % 3)
        {
            case 0:
                _player1.isControll = true;
                _player2.isControll = true;
                break;
            case 1:
                _player1.isControll = false;
                _player2.isControll = true;
                break;
            case 2:
                _player1.isControll = true;
                _player2.isControll = false;
                break;
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
        //_player1.Restart();
        //_player2.Restart();
        //child.Restart();
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
