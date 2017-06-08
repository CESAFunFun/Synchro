using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMap : MonoBehaviour {

    [SerializeField]
    private Character _spwanChild;

    [SerializeField]
    private GameObject _spwanPlayer;

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

            //１行目
            if (i == 0)
            {
                if (values[0] == "1")
                {
                    transform.parent.GetComponent<Stage>().useJump = true;
                }
                if (values[1] == "1")
                {
                    transform.parent.GetComponent<Stage>().usePlayerChanger = true;
                }
                if (values[2] == "1")
                {
                    transform.parent.GetComponent<Stage>().useGravity = true;
                }
                if (values[3] == "1")
                {
                    transform.parent.GetComponent<Stage>().usePlayerFloorFlip = true;
                }


                i++;
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
                            _spwanChild.transform.position = obj.transform.position;
                            _spwanChild.respawn = _spwanChild.transform.position;
                        }
                    }
                    sub.x += _scaleing * 1.25F;
                }
                sub.x = 0; sub.y -= _scaleing * 1.25F;
            }
        }
    }
}
