using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRote2 : MonoBehaviour
{

    [SerializeField]
    private float _speed;

    private bool _leftflag = false;

    private bool _rightflag = false;

    private float pos = 0.0f;

    public int count = 0;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            if(count<3)
                _leftflag = true;

        if (_leftflag)
        {
            if (pos < 45.0f)
            {
                pos += 1.0f * Time.deltaTime * _speed;
                transform.Rotate(Vector3.up * Time.deltaTime * _speed);
                count++;
            }
            else
            {
                pos = 0;
                _leftflag = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
            if (count > -3)
                _rightflag = true;

        if (_rightflag)
        {
            if (pos < 45.0f)
            {
                pos += 1.0f * Time.deltaTime * _speed;
                transform.Rotate(Vector3.down * Time.deltaTime * _speed);
                count--;
            }
            else
            {
                pos = 0;
                _rightflag = false;
            }
        }
    }
}
