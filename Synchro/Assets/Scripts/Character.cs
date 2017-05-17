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

    [SerializeField]
    private bool _downGravity = true;

    private const float G_POWER = 9.8F;

    private bool _isGround;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    private Vector3 _gravity;


	protected virtual void Start () {
        // 最初は必ず接地させないで
        _isGround = false;

        // 物理演算コンポーネントを取得して重力用の設定
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        // 移動量と重力方向の初期化
        _velocity = Vector3.zero;
        _gravity = Physics.gravity;
	}
	
	protected virtual void Update () {
        Debug.DrawLine(transform.position, transform.position + _gravity.normalized);
        _velocity = Vector3.zero;

        // 接地していなければ落下
        if (!_isGround)
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
        if (_isGround)
        {
            // 自身の上方向(重力方向の逆)に力を加える
            var jumpVec = -_gravity.normalized;
            _rigidbody.AddForce(jumpVec * power * 50F);
        }
    }

    public void ChangeGravity() {
        // 一度だけ宙に浮かせて反転させる
        _isGround = false;
        _downGravity = !_downGravity;
        // 重力を上方向か下方向に設定
        _gravity = _downGravity ? Vector3.down : Vector3.up;
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        // オブジェクトに接触したら下方にレイを飛ばして接地を判定する
        if (Physics.Linecast(transform.position, transform.position + _gravity.normalized))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
    }

    protected virtual void OnCollisionExit(Collision collision) {
        _isGround = false;
    }
}
