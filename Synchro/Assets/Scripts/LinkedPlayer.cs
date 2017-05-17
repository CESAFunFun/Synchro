// ==============================
// !@ biref プレイヤーを管理する
// !@ author Takayoshi Ogawa
// !@ since 2017/05/15
// ==============================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedPlayer : MonoBehaviour {

    public int playerNumber = 0;
    public Player[] players;

    [SerializeField]
    private RectTransform _turn;

    // Use this for initialization
    void Start () {
        ChangePlayer(playerNumber + 1, +1);
    }

    // Update is called once per frame
    void Update () {
        // 入力があれば操作を入れ替える
        foreach (var player in players)
        {
            if(player.gamepad.rightButton.trigger)
            {
                ChangePlayer(playerNumber, +1);
                break;
            }
            else if (player.gamepad.leftButton.trigger)
            {
                ChangePlayer(playerNumber, -1);
                break;
            }
        }
    }

    private void ChangePlayer(int temp, int side) {
        // 操作のOn/Off
        bool flag = true;
        // 次のプレイヤー
        int nextPlayer = temp + side;
        // 次のプレイヤーがない場合は最初へ
        if (nextPlayer > players.Length)
        {
            nextPlayer = 0;
        }
        else if(nextPlayer < 0)
        {
            nextPlayer = players.Length;
        }

        // 全てのプレイヤーが操作可能な状態
        if (nextPlayer == players.Length)
        {
            foreach (var player in players)
            {
                player.isControll = flag;
            }
        }
        else
        {
            // 全てのプレイヤーが動くか動かないかを判定する
            for (var i = 0; i < players.Length; i++)
            {
                if (i == nextPlayer)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                players[i].isControll = flag;
            }
        }

        // 現在の番号を保存
        playerNumber = nextPlayer;
        // 選択されている色が表示されるように回転
        _turn.eulerAngles = new Vector3(0F, 0F, playerNumber * 120F);
    }
}
