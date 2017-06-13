using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectManager : MonoBehaviour {

    [SerializeField]
    private SelectUI[] _stagenum;

    private Gamepad _gamepad;

    private int _number = 0;

    [SerializeField]
    float _waitTime;

    float _curremtTime;

    // Use this for initialization
    void Start()
    {
        _stagenum[_number].RotFlag = true;
    }
	// Update is called once per frame
	void Update () {

        _curremtTime += Time.deltaTime;

        //ゲームパッドの情報取得
        if (_gamepad == null)
            _gamepad = GetComponent<Gamepad>();
        else
        {
            if (_curremtTime < _waitTime) return;

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

            if (num != 0)
            {
                _stagenum[_number].RotFlag = false;
                _number += num;
               _number = Mathf.Clamp(_number, 0, _stagenum.Length-1);
                _stagenum[_number].RotFlag = true;
                _curremtTime = 0;
            }

            if(_gamepad.startButton.trigger)
            {
                GameManager.Instance.mapLevel = _number + 1;
                SceneManager.LoadScene("Play");
            }



        }



	}
}
