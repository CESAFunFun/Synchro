using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    [SerializeField]
    private GameObject[] stageObjects;

    [SerializeField]
    private float scaling = 1F;

    private int _oldLevel = -1;

    [SerializeField]
    private AudioClip _bgm;

    private void Start() {
        // 最初にマップのオブジェクトと大きさを設定
        for(var num = 0; num < transform.childCount; num++)
        {
            var child = transform.GetChild(num).GetComponent<CreateMap>();
            child.mapdate = stageObjects;
            child.scaling = scaling;
        }
        SoundManager.instance.PlayBGM(_bgm);
    }

    private void Update() {
        // 静的なクラスからレベルを取得
        var level = GameController.Instance.mapLevel;

        // 前フレームと同じなら以下の処理を省略
        if (level == _oldLevel) return;

        // 最終面のみマス数が異なるので強制的に修正
        if (level == GameController.Instance.levelMax)
            transform.position = new Vector3(-16.5F, 12.5F, 0F);

        UIAction.mapMoveBy = false;
        UIAction.mapChangeGravity = false;
        UIAction.mapBlinkGimmick = false;
        UIAction.mapBrokenGimmick = false;

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
