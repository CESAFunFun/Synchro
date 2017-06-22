using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCursor : MonoBehaviour {


    [SerializeField]
    private RectTransform[] _cursor;

    [SerializeField]
    private RectTransform _play;

    [SerializeField]
    private RectTransform _exit;

    [SerializeField]
    private string _sceneName;

    private bool _playflag = true;

    [SerializeField]
    private AudioClip _okSound;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //選択肢の変更
        if (GameController.Instance.gamepad.leftStick.y >= 0.5F)
        {
            if (!_playflag)
            {
                for (int i = 0; i < _cursor.Length; i++)
                {
                    Vector3 pos = _cursor[i].localPosition;
                    pos.y = _play.localPosition.y;
                    _cursor[i].localPosition = pos;
                }
            }
            _playflag = true;
        }

        if (GameController.Instance.gamepad.leftStick.y <= -0.5F)
        {
            if (_playflag)
            {
                for (int i = 0; i < _cursor.Length; i++)
                {
                    Vector3 pos = _cursor[i].localPosition;
                    pos.y = _exit.localPosition.y;
                    _cursor[i].localPosition = pos;
                }
            }
            _playflag = false;
        }

        //Aボタンでシーン移動
        if (GameController.Instance.gamepad.buttonA.trigger)
        {

            SoundManager.instance.PlaySFX(_okSound);
            if (_playflag) SceneManager.LoadScene(_sceneName);
            else Application.Quit();
        }
    }
}
