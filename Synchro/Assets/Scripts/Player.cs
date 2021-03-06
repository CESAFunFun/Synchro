﻿// ==============================
// !@ biref 操作可能なキャラクター
// !@ author Takayoshi Ogawa
// !@ since 2017/05/12
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [HideInInspector]
    public Gamepad gamepad;

    [HideInInspector]
    public bool isControll = true;

    [HideInInspector]
    public bool conectflag = false;

    [SerializeField]
    private Child _child;

    [SerializeField]
    private ParticleSystem _particle;

    //[SerializeField]
    //private AudioClip _jumpSFX;

    private LineRenderer _line;


    // Use this for initialization
    protected override void Start()
    {
        // キャラクターの初期化
        base.Start();
        conectflag = false;
        gamepad = GameController.Instance.gamepad;
        _line = GetComponent<LineRenderer>();
        if (_particle)
        {
            _particle.startColor = colorMat.color;
        }

        // スプライトの色を決定
        GetComponent<SpriteRenderer>().color = colorMat.color;
        GetComponent<TrailRenderer>().material = colorMat;
    }

    // Update is called once per frame
    protected override void Update()
    {
        // キャラクターの更新
        base.Update();

        // パートナーとの座標差分が一定内の場合に線描画を行うための処理
        ConectLine();

        // 操作可能かでマーカーの表示を変える
        transform.GetChild(0).gameObject.SetActive(isControll);

        // ここで入力を受け付けない
        if (!isControll) return;

        // 移動処理
        var moveX = gamepad.leftStick.x;
        moveX *= (CameraMove.FrontBack) ? 1F : -1F;
        Move(new Vector3(moveX, 0F, 0F), moveSpeed);

        // ジャンプ処理
        if (gamepad.buttonA.down)
        {
            if (canJump)
            {
                Jump(jumpPower);
            }
        }

        // 重力反転処理
        if (gamepad.rightButton.trigger)
        {
            if(canGravity)
                ChangeGravity();
        }
        // 足場反転処理
        if (gamepad.leftButton.trigger)
        {
            //if(canBlink)
            //    BlinkPosition();
        }
    }

    private void ConectLine()
    {
        if ((transform.position.x - _child.transform.position.x)*(transform.position.x - _child.transform.position.x)
            +(transform.position.y - _child.transform.position.y)*(transform.position.y - _child.transform.position.y)
            <=(0.5f+0.5f)*(0.5f+0.5f))
        {
            conectflag = true;
        }
        else
        {
            // 一定範囲の外なので処理をしない
            conectflag = false;
        }
        // 子要素を見つけたら線をつなげる
        if (conectflag)
        {
            conectflag = true;
            _line.SetPosition(0, transform.position);
            _line.SetPosition(1, _child.transform.position);
        }
        // 繋がっていたら描画を有効化
        _line.enabled = conectflag;
    }

    public override void Restart()
    {
        //conectflag = false;
        // 基底関数を呼ぶ
        base.Restart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            if (conectflag)
            {
                // パーティクルの位置と角度を修正
                _particle.transform.eulerAngles = (downGravity) ?
                    Vector3.left * 90F : Vector3.left * 270F;
                _particle.transform.position = transform.position;
                //_particle.transform.position += _gravity * -10F;
                // パーティクルを再生
                //_particle.Play();
            }
            GameObject.Find("GameManager").SendMessage("Fail");
            Restart();
        }
    }
}
