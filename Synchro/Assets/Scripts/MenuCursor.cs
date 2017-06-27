using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCursor : MonoBehaviour
{

    [SerializeField]
    private RectTransform _cursor;

    [SerializeField]
    private RectTransform[] _menu;

    [SerializeField]
    private string keep;

    private Vector3 pos;

    [SerializeField]
    private string[] _scenesName;

    private int _sceneNumber = 0;
    private int _recastCount = 0;

    [SerializeField]
    private AudioClip _cursorSound;

    // Update is called once per frame
    void Update()
    {

        // メニュー項目に位置を修正
        _sceneNumber = Mathf.Clamp(_sceneNumber, 0, _menu.Length - 1);
        pos = _cursor.localPosition;
        pos.y = _menu[_sceneNumber].localPosition.y;
        _cursor.localPosition = pos;

        //Aボタンでシーン移動
        if (GameController.Instance.gamepad.buttonA.trigger)
        {
            //藤井が修正
            if (_menu[_sceneNumber].name == "Next")
            {
                if (GameController.Instance.mapLevel < GameController.Instance.levelMax)
                {
                    GameController.Instance.mapLevel++;
                    //SceneManager.LoadScene(_scenesName[_sceneNumber]);
                }
            }
            //else
            SceneManager.LoadScene(_scenesName[_sceneNumber]);
        }

        //最終ステージではネクストステージを消す
        if (GameController.Instance.mapLevel != GameController.Instance.levelMax)
        {
            _menu[0].GetComponent<UnityEngine.UI.Text>().text = keep;
            _scenesName[0] = "Play";
        }
        else
        {
            _menu[0].GetComponent<UnityEngine.UI.Text>().text = "Title";
            _scenesName[0] = "Title";
            GameController.Instance.mapLevel = 1;
        }

        // 選択肢を上に変更
        if (GameController.Instance.gamepad.leftStick.y >= 0.5F)
        {
            if (_recastCount++ <= 30)
            {
                _sceneNumber--;
                _recastCount = 0;
                SoundManager.instance.PlaySFX(_cursorSound);
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
                SoundManager.instance.PlaySFX(_cursorSound);
            }
        }
        else
        {
            _recastCount = 0;
        }
    }
}
