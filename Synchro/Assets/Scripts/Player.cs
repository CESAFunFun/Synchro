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

    public void Controll(bool flag)
    {
        isControll = flag;
    }

    [SerializeField]
    private Player partner;

    [SerializeField]
    private static Character child;

    private static bool conectflag = false;

    public static bool conectFlag { get { return conectflag; } }
    // Use this for initialization
    protected override void Start () {
        // キャラクターの初期化
        base.Start();
        transform.name = "stupid";
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
        // 足場反転処理
        if (gamepad.leftButton.trigger)
        {
            if (isGround)
            {
                BlinkPosition();
            }
        }

        //npcが間にいるか判定
        if (!child)
        {
            var dis = Vector3.Distance(transform.position, partner.transform.position);
            var dir = partner.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, dis))
            {
                if (hit.transform.tag == "Child")
                {
                    child = hit.transform.gameObject.GetComponent<Character>();
                }
            }
        }
        if(child)
        {
            //中間地点にnpcを移動させる
            var vec = partner.transform.position - transform.position;

            if ((vec.x <= -0.25F || vec.x >= 0.25F) || (vec.y <= -1F || vec.y >= 1F))
            {
                conectflag = false;
                return;
            }
            conectflag = true;
            vec = transform.position + vec / 2;
            child.transform.position = new Vector3(vec.x,vec.y, child.transform.position.z);
            child.GetComponent<Character>().downGravity = GetComponent<Character>().downGravity;
        }
    }

    public override void Restart()
    {
        base.Restart();

        child = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            if (child)
            {
                child.Restart();
                child = null;
            }
            Restart();
            partner.Restart();
        }
    }
}
