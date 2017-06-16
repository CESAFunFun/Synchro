using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    private Gamepad gamepad;

    private bool first = false;

    private ParticleSystem particle;
    protected override void Start()
    {
        base.Start();
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        gamepad = GameController.Instance.gamepad;
    }

    protected override void Update()
    {
        base.Update();

        if(players[0].conectflag && players[1].conectflag)
        {
            if(!first)
            {
                //一回のみ
                var pos = transform.position;
                pos.z = players[0].transform.position.z;
                players[0].transform.position = pos;
                pos.z = players[1].transform.position.z;
                players[1].transform.position = pos;
                first = true;
            }
            //if (players[0].conectflag)
            //{
            //    downGravity = players[0].downGravity;
            //    transform.position = new Vector3(
            //        players[0].transform.position.x,
            //        players[0].transform.position.y,
            //        this.transform.position.z);
            //}

            //if (players[1].conectflag)
            //{
            //    downGravity = players[1].downGravity;
            //    transform.position = new Vector3(
            //        players[1].transform.position.x,
            //        players[1].transform.position.y,
            //        this.transform.position.z);
            //}

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

            particle.startRotation++;
        }
        else
        {
            first = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            GameObject.Find("GameManager").SendMessage("Fail");
        }
    }
}
