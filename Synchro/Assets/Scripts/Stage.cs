using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    [SerializeField]
    private GameObject[] stageObjects;

    [SerializeField]
    private float scaling = 1F;

    private int _oldLevel = -1;

    private void Start() {
        // 最初にマップのオブジェクトと大きさを設定
        for(var num = 0; num < transform.childCount; num++)
        {
            var child = transform.GetChild(num).GetComponent<CreateMap>();
            child.mapdate = stageObjects;
            child.scaling = scaling;
        }
    }

    private void Update() {
        // 静的なクラスからレベルを取得
        var level = GameController.Instance.mapLevel;

        // 前フレームと同じなら以下の処理を省略
        if (level == _oldLevel) return;

        // マップを一度削除してから生成する
        for (var num = 0; num < transform.childCount; num++)
        {
            var map = transform.GetChild(num).GetComponent<CreateMap>();
            map.Remove();
            map.Make(level);
        }

        // 書き換えが一度きりになるように設定
        _oldLevel = level;
    }
}
