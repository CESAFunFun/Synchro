using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMap : MonoBehaviour {

    [SerializeField]
    private TextAsset[] mapdata;

    private GameObject[] mapchip;
    private int currentMapLevel;

	// Use this for initialization
	void Start () {
        // 親オブジェクト内のステージからオブジェクトとレベルし作成
        var stage = transform.parent.GetComponent<Stage>();
        mapchip = stage.stageObjects;
        this.ReMakeMap(stage.stageLevel);
    }

    public void ReMakeMap(int mapLevel) {
        // マップチップ１つ分の差分は埋める
        float scaling = 0F;
        Vector2 sub = Vector2.zero;

        // 指定されたレベルのテキストを読み込みマップの作成
        StringReader reader = new StringReader(mapdata[mapLevel - 1].text);
        while (reader.Peek() > -1)
        {
            var line = reader.ReadLine();
            var values = line.Split(',');
            foreach (string value in values)
            {
                int integer = int.Parse(value);
                if (integer >= 0 && integer < mapchip.Length)
                {
                    var position = transform.position;
                    var obj = Instantiate(mapchip[integer], transform);
                    obj.transform.position = new Vector3(position.x + sub.x, position.y + sub.y, position.z);
                    obj.transform.localScale = Vector3.one * 1F;
                    scaling = obj.transform.localScale.x;
                }
                sub.x += (scaling + scaling * 0.25F);
            }
            sub.x = 0F; sub.y -= (scaling + scaling * 0.25F);
        }
    }
}
