using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChengeSpriteNum : MonoBehaviour {

    [SerializeField]
    private Sprite[] _numbers;

    [SerializeField]
    private GameObject[] _mirisecondSprite;

    [SerializeField]
    private GameObject[] _secondSprite;

    [SerializeField]
    private GameObject[] _minuteSprite;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        int seconds = GetComponent<Timer>().seconds;
        int minute = GetComponent<Timer>().minute;
        float mirisedond = GetComponent<Timer>().countTime;
        mirisedond *= 100;
        int miri = (int)mirisedond;

        var seco = Digit(seconds);
        var minu = Digit(minute);
        var miriseco = Digit(miri);
        for(int i=0;i<2;i++)
        {
            _secondSprite[i].GetComponent<SpriteRenderer>().sprite = _numbers[seco[i]];
            _minuteSprite[i].GetComponent<SpriteRenderer>().sprite = _numbers[minu[i]];
            _mirisecondSprite[i].GetComponent<SpriteRenderer>().sprite = _numbers[miriseco[i]];
        }
    }

    int[] Digit(int num)
    {
        int[] digit = new int[2];

        digit[0] = num / 10;
        digit[1] = num % 10;

        return digit;
    }
}
