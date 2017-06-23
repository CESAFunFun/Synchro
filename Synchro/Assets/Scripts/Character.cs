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

    //[HideInInspector]
    public bool isGround = false;
    [HideInInspector]
    public bool downGravity = true;
    [HideInInspector]
    public Vector3 respawn;

    [HideInInspector]
    public bool canJump = false;
    [HideInInspector]
    public bool canChange = false;
    [HideInInspector]
    public bool canGravity = false;
    [HideInInspector]
    public bool canBlink = false;

    private const float G_POWER = 9.8F;
    private const float G_LENGTH = 0.6F;

    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Vector3 _gravity;

    private bool _skyChange;

    [SerializeField]
    protected Material colorMat;

    [SerializeField]
    private AudioClip _jumpSFX;

    [SerializeField]
    private AudioClip _gravitySFX;

    public Material colorMaterial { get { return colorMat; } }


    protected virtual void Start () {
        // 重力関連のフラグを初期化
        isGround = false;
        downGravity = true;
        _skyChange = true;

        // 物理演算コンポーネントを取得して重力用の設定
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        // 最初の位置を保存
        respawn = transform.position;

        // 移動量の初期化
        _velocity = Vector3.zero;
        // 重力を上方向か下方向に設定
        _gravity = downGravity ? Vector3.down : Vector3.up;
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
            // ジャンプ音を再生
            if(_jumpSFX) SoundManager.instance.PlaySFX(_jumpSFX);
        }
    }

    public void ChangeGravity() {
        if (isGround)
        {
            // 一度だけ宙に浮かせて反転させる
            isGround = false;
            downGravity = !downGravity;
            SoundManager.instance.PlaySFX(_gravitySFX);
        }
        else if(_skyChange)
        {
            isGround = false;
            downGravity = !downGravity;
            _skyChange = false;
            SoundManager.instance.PlaySFX(_gravitySFX);
        }
    }

    public void BlinkPosition() {
        if (isGround)
        {
            // 位置を入れ替えるための原点（重力の原点）
            var point = transform.position + _gravity;
            transform.RotateAround(point, Vector3.left, 180F);
            downGravity = !downGravity;
        }
    }

    public virtual void Restart() {
        // (親がいれば親の)位置座標を最初に戻す

        if (transform.parent != null)
        {
            transform.parent.position = respawn;
        }
        else
        {
            transform.position = respawn;
        }

        // このクラスの初期化処理を実行
        this.Start();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            _skyChange = true;

            // オブジェクトに接触したら下方にレイを飛ばして接地を判定する
            if (Physics.Linecast(transform.position, transform.position + _gravity.normalized * G_LENGTH))
            {
                isGround = true;
                _rigidbody.velocity = Vector3.zero;
            }
            else
            {
                isGround = false;
            }
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
            }
            else
            {
                isGround = false;
            }
        }
    }

    protected virtual void OnCollisionExit(Collision collision) {
        isGround = false;
    }
}