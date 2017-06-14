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

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform[] _cameraPos;

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
                GameController.Instance.mapLevel = _number;
               _number = Mathf.Clamp(_number, 0, _stagenum.Length-1);
                _stagenum[_number].RotFlag = true;
                _curremtTime = 0;
            }

            if(_gamepad.startButton.trigger)
            {
                GameController.Instance.mapLevel = _number;
                SceneManager.LoadScene("Play");
            }



        }

        if (_number <= 4 && _number >= 0)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[0].position, 1.0f);
        }
        else if (_number <= 9 && _number >= 5)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[1].position, 1.0f);
        }
        else
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _cameraPos[2].position, 1.0f);
        }
	}
}
