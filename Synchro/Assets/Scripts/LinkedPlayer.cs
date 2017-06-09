// ==============================
// !@ biref プレイヤーを管理する
// !@ author Takayoshi Ogawa
// !@ since 2017/05/15
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPlayer : MonoBehaviour {

    [HideInInspector]
    public Gamepad gamepad;

    [SerializeField]
    private Player[] player;

    [SerializeField]
    private RectTransform _turn;

    private int playerNumber = 2;

    //private LineRenderer renderer;

    // Use this for initialization
    void Start () {
        gamepad = GameController.Instance.gamepad;
        //renderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {
        //renderer.enabled = (Player.conectflag) ? true : false;
        //renderer.SetPosition(0, player[0].transform.position);
        //renderer.SetPosition(1, player[1].transform.position);

        if (gamepad.buttonB.trigger)
        {
            player[0].isControll = true;
            player[1].isControll = false;
            playerNumber = 0;
        }
        else if (gamepad.buttonX.trigger)
        {
            player[0].isControll = false;
            player[1].isControll = true;
            playerNumber = 1;
        }
        else if (gamepad.buttonY.trigger)
        {
            player[0].isControll = true;
            player[1].isControll = true;
            playerNumber = 2;
        }

        // 選択されている色が表示されるように回転
        _turn.eulerAngles = new Vector3(0F, 0F, playerNumber * 120F);
    }
}
