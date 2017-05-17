// ==============================
// !@ biref 操作可能なキャラクター
// !@ author Takayoshi Ogawa
// !@ since 2017/05/12
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    [HideInInspector]
    public bool isControll = true;
    [HideInInspector]
    public Gamepad gamepad;

    [SerializeField]
    private UnityEngine.UI.Image garge;

	// Use this for initialization
	protected override void Start () {
        // キャラクターの初期化
        base.Start();
        // ゲームパッドを取得
        gamepad = GetComponent<Gamepad>();
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

        // 円ゲージを一定の位置に固定
        garge.canvas.transform.rotation = Quaternion.Euler(0F, 0F, 0F);

        // 重力反転処理
        if(gamepad.buttonX.down)
        {
            garge.fillAmount += 0.02F;

            if (garge.fillAmount >= 1F)
            {
                // 円ゲージの値を初期化
                garge.fillAmount = 0F;
                // 重力の変更
                ChangeGravity();
            }
        }
        else
        {
            // 押されていないときの円ゲージの値
            garge.fillAmount = 0F;
        }
    }
}
