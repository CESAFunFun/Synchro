using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMap : MonoBehaviour {

    [SerializeField]
    private Character _character;

    [SerializeField]
    private float _scaleing = 1F;

    [SerializeField]
    private TextAsset[] _mapchip;

    private GameObject[] _mapdate;



    // Use this for initialization
    void Start() {
        int i = 0;
        // 親オブジェクトのステージからリソースを取得
        var stage = transform.parent.GetComponent<Stage>();
        _mapdate = stage.stageObjects;

        Vector3 sub = Vector3.zero;

        // テキストからマップデータを読み込み
        StringReader reader = new StringReader(_mapchip[stage.stageLevel].text);
        while (reader.Peek() > -1)
        {
            // カンマ区切りで読み込んで行ごとにマップを作成
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            //１行目のみ
            if (i++ == 0)
            {
                _character.canJump = (values[0] == "0") ? false : true;
                //_character.canChange = (values[1] == "0") ? false : true;
                _character.canGravity = (values[2] == "0") ? false : true;
                _character.canBlink = (values[3] == "0") ? false : true;
            }
            else
            {
                // 2行目以降
                foreach (string value in values)
                {
                    // 読み込んだからマップを作成
                    int integer = int.Parse(value);
                    if (integer >= 0 && integer < _mapdate.Length)
                    {
                        // 位置座標の差分を加味してリソースを配置
                        var obj = Instantiate(_mapdate[integer], transform);
                        obj.transform.position = transform.position + sub;
                        obj.transform.localScale *= _scaleing;

                        if(integer == 1)
                        {
                            _character.transform.position = obj.transform.position;
                            _character.respawn = _character.transform.position;
                        }
                    }
                    sub.x += _scaleing * 1.25F;
                }
                sub.x = 0; sub.y -= _scaleing * 1.25F;
            }
        }
    }
}
