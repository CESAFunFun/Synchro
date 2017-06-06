// ==============================
// !@ biref ゲーム内のキャラクター
// !@ author Takayoshi Ogawa
// !@ since 2017/05/12
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour {

    public float moveSpeed = 1F;
    public float jumpPower = 1F;
    public bool isGround = false;
    public bool downGravity = true;

    [HideInInspector]
    public Vector3 _respawn;

    private const float G_POWER = 9.8F;
    private const float G_LENGTH = 1F;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Vector3 _gravity;

    private bool _skyChange;

	protected virtual void Start () {
        // 重力関連のフラグを初期化
        isGround = false;
        downGravity = true;
        _skyChange = true;

        // 物理演算コンポーネントを取得して重力用の設定
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        // 移動量の初期化
        _velocity = Vector3.zero;
        // 重力を上方向か下方向に設定
        _gravity = downGravity ? Vector3.down : Vector3.up;

        // 最初の位置を保存しておく
        _respawn = transform.position;
    }
	
	protected virtual void Update () {
        // 重力方向の線を描画
        Debug.DrawLine(transform.position, transform.position + _gravity.normalized * G_LENGTH);

        // 移動量の初期化
        _velocity = Vector3.zero;
        // 重力を上方向か下方向に設定
        _gravity = downGravity ? Vector3.down : Vector3.up;

        // 接地していなければ落下
        if (!isGround)
        {
            _rigidbody.AddForce(_gravity.normalized * G_POWER);
        }
	}

    private void FixedUpdate() {
        // 物理演算コンポーネントによる移動処理
        _rigidbody.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    public void Move(Vector3 velocity, float speed) {
        // 移動量と速度を更新
        _velocity = velocity * speed;
    }

    public void Jump(float power) {
        // 接地していた場合のみ
        if (isGround)
        {
            isGround = false;
            // 自身の上方向(重力方向の逆)に力を加える
            var jumpVec = -_gravity.normalized;
            _rigidbody.AddForce(jumpVec * power * 50F);
        }
    }

    public void ChangeGravity(bool isGround = false) {
        if (this.isGround)
        {         // 一度だけ宙に浮かせて反転させる
            this.isGround = isGround;
            downGravity = !downGravity;
        }
        else if(_skyChange)
        {
            this.isGround = isGround;
            downGravity = !downGravity;
            _skyChange = false;
        }
    }

    public void BlinkPosition()
    {
        // 位置を入れ替えるための原点（重力の原点）
        var point = transform.position + _gravity;
        transform.RotateAround(point, Vector3.left, 180F);
        ChangeGravity(true);
    }

    public virtual void Restart() {
        // (親がいれば親の)位置座標を最初に戻す

        if (transform.parent != null)
        {
            transform.parent.position = _respawn;
        }
        else
        {
            transform.position = _respawn;
        }

        // このクラスの初期化処理を実行
        this.Start();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            _skyChange = true;
        }
    }

    protected virtual void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            // オブジェクトに接触したら下方にレイを飛ばして接地を判定する
            if (Physics.Linecast(transform.position, transform.position + _gravity.normalized * G_LENGTH))
            {
                if (transform.position.y > collision.transform.position.y
                && _gravity == Vector3.down)
                    isGround = true;
                else if (transform.position.y < collision.transform.position.y
                    && _gravity == Vector3.up)
                    isGround = true;
                else
                    isGround = false;
            }

            //// オブジェクトに接触したら下方にレイを飛ばして接地を判定する
            //if (Physics.Linecast(transform.position, transform.position + _gravity.normalized * G_LENGTH))
            //{
            //    isGround = true;
            //}
            //else
            //{
            //    isGround = false;
            //}
        }
    }

    protected virtual void OnCollisionExit(Collision collision) {
        isGround = false;
    }
}