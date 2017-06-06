// ==============================
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

    [SerializeField]
    private Player partner;

    //private static Character child;

    [HideInInspector]
    public bool conectflag;

    private LineRenderer _line;

    // Use this for initialization
    protected override void Start()
    {
        // キャラクターの初期化
        base.Start();
        gamepad = GameManager.Instance.gamePad;
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    protected override void Update()
    {
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
        if (gamepad.rightButton.trigger)
        {
            ChangeGravity();
        }
        // 足場反転処理
        if (gamepad.leftButton.trigger)
        {
            if (isGround)
            {
                BlinkPosition();
            }
        }
        
        // パートナーとの座標差分が一定内の場合に線描画を行うための処理
        var dir = partner.transform.position - transform.position;
        if ((dir.x >= -0.5F && dir.x <= 0.5F) && (dir.y >= -0.5F && dir.y <= 0.5F))
        {
            var dis = Vector3.Distance(transform.position, partner.transform.position);

            RaycastHit hit;
            if(Physics.Raycast(transform.position, dir, out hit, dis))
            {
                if(hit.transform.tag == "Child")
                {
                    // NPCと接触した場合に成立
                    _line.enabled = true;
                    _line.SetPosition(0, transform.position);
                    _line.SetPosition(1, hit.transform.position);
                }
                else
                {
                    // 接触していない場合に不成立
                    _line.enabled = false;
                }
            }
        }
        else
        {
            // 一定の差分外にいる場合も不成立にする
            _line.enabled = false;
        }

        // フラグが成立している場合のみ線を描画
        conectflag = _line.enabled;
    }

    public override void Restart()
    {
        //child = null;
        base.Restart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            Restart();
            partner.Restart();
            conectflag = false;
        }
    }
}
