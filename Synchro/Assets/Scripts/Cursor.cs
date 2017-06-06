using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    private int _number = 8;

    [SerializeField]
    private float _speed = 1F;

    [SerializeField]
    private string _nextSceneName;

    [SerializeField]
    private Vector2 _lerpLength = Vector2.one;

    private Gamepad _gamepad;
    private RectTransform _rectTransform;
    private bool _isLerp;
    private float _startTime;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    // Use this for initialization
    void Start()
    {
        _gamepad = GameManager.Instance.gamePad;
        _rectTransform = GetComponent<RectTransform>();
        _isLerp = false;
        _startPosition = transform.position;
        _endPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _rectTransform.Rotate(Vector3.forward * 3F);

        if (_isLerp)
        {
            var diff = Time.timeSinceLevelLoad - _startTime;
            if (diff > 1F)
            {
                _rectTransform.position = _endPosition;
                _isLerp = false;
            }

            _rectTransform.position = transform.position = Vector3.Lerp(_startPosition, _endPosition, diff / 1F * _speed);
        }
        else
        {
            _startTime = Time.timeSinceLevelLoad;
            _startPosition = _rectTransform.position;
            if (_gamepad == null)
                _gamepad = GameManager.Instance.gamePad;
            else
            {
                if (_gamepad.leftStick.x <= -1F)
                {
                    _endPosition += Vector3.left * _lerpLength.x;
                    _number += -1;
                    _isLerp = true;
                }
                if (_gamepad.leftStick.x >= 1F)
                {
                    _endPosition += Vector3.right * _lerpLength.x;
                    _number += +1;
                    _isLerp = true;
                }

                if (_gamepad.leftStick.y <= -1F)
                {
                    _endPosition += Vector3.down * _lerpLength.y;
                    _number += +5;
                    _isLerp = true;
                }
                if (_gamepad.leftStick.y >= 1F)
                {
                    _endPosition += Vector3.up * _lerpLength.y;
                    _number += -5;
                    _isLerp = true;
                }

                if (_gamepad.startButton.trigger)
                {
                    GameManager.Instance.mapLevel = _number;
                    SceneManager.LoadScene("Play");
                }
            }
        }
    }
}
