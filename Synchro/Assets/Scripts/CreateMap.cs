using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMap : MonoBehaviour {

    [SerializeField]
    private float _scaleing = 1F;

    [SerializeField]
    private TextAsset[] _mapchip;

    private GameObject[] _mapdate;

    // Use this for initialization
    void Start() {
        // 親オブジェクトのステージからリソースを取得
        var stage = transform.parent.GetComponent<Stage>();
        _mapdate = stage.stageObjects;

        Vector3 sub = Vector3.zero;

        // テキストからマップデータを読み込み
        StringReader reader = new StringReader(_mapchip[stage.stageLevel - 1].text);
        while (reader.Peek() > -1)
        {
            // カンマ区切りで読み込んで行ごとにマップを作成
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            foreach (string value in values)
            {
                // 読み込んだからマップを作成
                int integer = int.Parse(value);
                if (integer >= 0 && integer < _mapdate.Length)
                {
                    // 位置座標の差分を加味してリソースを配置
                    var obj = Instantiate(_mapdate[integer], transform);
                    obj.transform.position = transform.position + sub;
                    obj.transform.localScale = Vector3.one * _scaleing;
                }
                sub.x += (int)(_scaleing * 1.5F);
            }
            sub.x = 0; sub.y -= (int)(_scaleing * 1.5F);
        }
    }
}
