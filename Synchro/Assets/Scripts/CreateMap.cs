using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMap : MonoBehaviour {

    [SerializeField]
    private Character _character;

    [SerializeField]
    private TextAsset[] _mapchip;

    [HideInInspector]
    public GameObject[] mapdate;

    [HideInInspector]
    public float scaling = 1F;


    private void Start() {
        GameController.Instance.levelMax = _mapchip.Length;
    }

    public void Make(int stageLevel) {
        int row = 0;
        Vector3 sub = Vector3.zero;
        if(_character != null) _character.Restart();

        // テキストからマップデータを読み込み
        StringReader reader = new StringReader(_mapchip[stageLevel].text);
        while (reader.Peek() > -1)
        {
            // カンマ区切りで読み込んで行ごとにマップを作成
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            //１行目のみ
            if (row++ == 0)
            {
                if (_character != null)
                {
                    _character.canJump = (values[0] == "0") ? false : true;
                    _character.canChange = (values[1] == "0") ? false : true;
                    _character.canGravity = (values[2] == "0") ? false : true;
                    //_character.canBlink = (values[3] == "0") ? false : true;
                }
            }
            else
            {
                // 2行目以降
                foreach (string value in values)
                {
                    // 読み込んだからマップを作成
                    int integer = int.Parse(value);
                    if (integer >= 0 && integer < mapdate.Length)
                    {
                        // 位置座標の差分を加味してリソースを配置
                        var obj = Instantiate(mapdate[integer], transform);
                        obj.transform.position = transform.position + sub;
                        obj.transform.localScale *= scaling;

                        switch (integer)
                        {
                            case 0:
                                obj.GetComponent<SpriteRenderer>().color = _character.colorMaterial.color;
                                break;
                            case 1:
                                if (_character != null)
                                {
                                    _character.transform.position = obj.transform.position;
                                    _character.respawn = _character.transform.position;
                                }
                                break;
                            case 2:
                                break;
                            case 3:
                                UIAction.mapMoveBy = true;
                                break;
                            case 4:
                                UIAction.mapChangeGravity = true;
                                break;
                            case 5:
                                UIAction.mapBrokenGimmick = true;
                                break;
                            case 6:
                                UIAction.mapBlinkGimmick = true;
                                break;
                            default:
                                break;
                        }
                    }
                    sub.x += scaling * 1.25F;
                }
                sub.x = 0; sub.y -= scaling * 1.25F;
            }
        }
    }

    public void Remove() {
        for(var num = 0; num < transform.childCount; num++)
        {
            Destroy(transform.GetChild(num).gameObject);
        }
    }
}
