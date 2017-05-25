// ==============================
// !@ biref 操作可能なキャラクター
// !@ author Takayoshi Ogawa
// !@ since 2017/05/12
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public Gamepad gamepad;

    [HideInInspector]
    public bool isControll = true;

	// Use this for initialization
	protected override void Start () {
        // キャラクターの初期化
        base.Start();
	}

    // Update is called once per frame
    protected override void Update() {
        // キャラクターの更新
        base.Update();

        // ここで入力を受け付けない
        if (!isControll) return;

        // 移動処理
        Move(new Vector3(gamepad.leftStick.x, 0F, 0F), moveSpeed);

        // ジャンプ処理
        if (gamepad.buttonA.down)
        {
            Jump(jumpPower);
        }

        // 重力反転処理
        if(gamepad.rightButton.trigger)
        {
            if (isGround)
            {
                ChangeGravity();
            }
        }
    }
}
