using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour {

    [SerializeField]
    private RectTransform _cursor;

    [SerializeField]
    private RectTransform[] _menu;

    [SerializeField]
    private string[] _scenesName;
    
    private int _sceneNumber = 0;
    private int _recastCount = 0;

    // Update is called once per frame
    void Update() {
        // 選択肢を上に変更
        if (GameController.Instance.gamepad.leftStick.y >= 0.5F)
        {
            if (_recastCount++ <= 30)
            {
                _sceneNumber--;
                _recastCount = 0;
            }
        }
        else
        {
            _recastCount = 0;
        }

        // 選択肢を下に変更
        if (GameController.Instance.gamepad.leftStick.y <= -0.5F)
        {
            if (_recastCount++ <= 30)
            {
                _sceneNumber++;
                _recastCount = 0;
            }
        }
        else
        {
            _recastCount = 0;
        }

        // メニュー項目に位置を修正
        _sceneNumber = Mathf.Clamp(_sceneNumber, 0, _menu.Length - 1);
        Vector3 pos = _cursor.localPosition;
        pos.y = _menu[_sceneNumber].localPosition.y;
        _cursor.localPosition = pos;


        //Aボタンでシーン移動
        if (GameController.Instance.gamepad.buttonA.trigger)
        {
            if (_scenesName[_sceneNumber] != "")
            {
                SceneManager.LoadScene(_scenesName[_sceneNumber]);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
