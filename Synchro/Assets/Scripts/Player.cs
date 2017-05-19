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
    private UnityEngine.UI.Image gravityGarge;
    [SerializeField]
    private UnityEngine.UI.Image blinkGarge;

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
        

        // 重力反転処理
        if (Charge(gamepad.rightButton.down, gravityGarge))
        {
            ChangeGravity();
        }

        // 座標転移
        if (Charge(gamepad.leftButton.down, blinkGarge))
        {
            if(_isGround) BlinkPosition();
        }
    }

    bool Charge(bool button, UnityEngine.UI.Image image) {
        image.canvas.transform.rotation = Quaternion.Euler(0F, 0F, 0F);

        if (button)
        {
            image.fillAmount += 0.02F;

            if (image.fillAmount >= 1F)
            {
                image.fillAmount = 0F;
                return true;
            }
        }
        else
        {
            image.fillAmount = 0F;
        }

        return false;
    }
}
