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

    //private Character _child;

    [HideInInspector]
    public bool conectflag = false;

    private LineRenderer _line;

    private Material colorMat;

    public Material colorMaterial { get { return colorMat; } }

    // Use this for initialization
    protected override void Start()
    {
        // キャラクターの初期化
        base.Start();
        gamepad = GameController.Instance.gamepad;
        _line = GetComponent<LineRenderer>();
        //_child = null;

        // スプライトの色を決定
        GetComponent<SpriteRenderer>().color = color;
        // トレイルの色（マテリアル）を決定
        colorMat = new Material(Shader.Find("Standard"));
        colorMat.SetOverrideTag("RenderType", "Transparent");
        colorMat.SetFloat("_Glossiness", 0F);
        colorMat.SetFloat("_Metallic", 0F);
        colorMat.SetColor("_Color", color);
        colorMat.SetColor("_EmissionColor", color);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // キャラクターの更新
        base.Update();

        // ここで入力を受け付けない
        if (!isControll) return;

        // 移動処理
        var moveX = gamepad.leftStick.x;
        moveX *= (CameraMove.FrontBack) ? 1F : -1F;
        Move(new Vector3(moveX, 0F, 0F), moveSpeed);

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
            //BlinkPosition();
        }

        // パートナーとの座標差分が一定内の場合に線描画を行うための処理
        ConectLine(partner.transform.position - transform.position);
    }

    private void ConectLine(Vector3 direction)
    {
        // パートナーとの一定範囲内ならば処理を開始する
        if((direction.x >= -(transform.localScale.x / 2F) * 05F && direction.x <= (transform.localScale.x / 2F) * 05F
         && direction.y >= -(transform.localScale.y / 2F) * 18F && direction.y <= (transform.localScale.y / 2F) * 18F))
        {
            // 子要素を見つけるためにRaycastを行う
            if(!/*_child*/conectflag)
            {
                RaycastHit hit;
                var dis = Vector3.Distance(transform.position, partner.transform.position);
                if(Physics.Raycast(transform.position, direction, out hit, dis))
                {
                    if(hit.transform.tag == "Child")
                    {
                        //_child = hit.transform.GetComponent<Child>();
                        conectflag = true;
                    }
                }
            }

            //// 子要素を見つけたら線をつなげる
            //if(_child && isControll)
            //{
            //    conectflag = true;
            //    _line.SetPosition(0, transform.position);
            //    _line.SetPosition(1, _child.transform.position);
            //
            //    // パートナーとつながっていたら移動処理
            //    if(partner.conectflag && partner.isControll)
            //    {
            //        var sub = transform.position + direction / 2F;
            //        _child.downGravity = downGravity;
            //        _child.transform.position = new Vector3(
            //            sub.x, sub.y, _child.transform.position.z);
            //    }
            //}
        }
        else
        {
            // 一定範囲の外なので処理をしない
            //_child = null;
            conectflag = false;
        }

        // 繋がっていたら描画を有効化
        _line.enabled = conectflag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            //_child = null;
            Restart();
            partner.Restart();
            conectflag = false;
        }
    }
}
