using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Gamepad gamepad;

    private bool goalFlag = false;
    private int playerNumber = 2;

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private Child child;
    [SerializeField] private RectTransform turn;

    private void Start()
    {
        gamepad = GameController.Instance.gamepad;
    }

    private void Update() {

        // 操作キャラクターの変更
        if (player1.canChange && player2.canChange)
        {
            if (gamepad.buttonB.trigger)
            {
                player1.isControll = true;
                player2.isControll = false;
                playerNumber = 0;
            }
            else if (gamepad.buttonX.trigger)
            {
                player1.isControll = false;
                player2.isControll = true;
                playerNumber = 1;
            }
            else if (gamepad.buttonY.trigger)
            {
                player1.isControll = true;
                player2.isControll = true;
                playerNumber = 2;
            }
        }

        // 選択されている色が表示されるように回転
        turn.eulerAngles = new Vector3(0F, 0F, playerNumber * 120F);

        // クリアしたらSelectへ遷移
        if (gamepad.startButton.trigger)
        {
            if (goalFlag)
            {
                goalFlag = false;
                SceneManager.LoadScene("Select2");
            }
            else
            {
                Pose();
            }
        }
    }

    private void Clear()
    {
        goalFlag = true;
    }

    private void Fail()
    {
        player1.Restart();
        player2.Restart();
        child.Restart();
    }

    private void OnGUI() {
        if (goalFlag)
        {
            GUI.TextArea(new Rect(Screen.width / 2F, Screen.height / 2F, 75, 50), "Clear!");
        }
    }

    private void Pose()
    {
        player1.enabled = !player1.enabled;
        player1.GetComponent<Rigidbody>().isKinematic = !player1.GetComponent<Rigidbody>().isKinematic;
        player2.enabled = !player2.enabled;
        player2.GetComponent<Rigidbody>().isKinematic = !player2.GetComponent<Rigidbody>().isKinematic;
        child.enabled = !child.enabled;
        child.GetComponent<Rigidbody>().isKinematic = !child.GetComponent<Rigidbody>().isKinematic;
    }

}
