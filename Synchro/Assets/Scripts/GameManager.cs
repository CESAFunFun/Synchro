using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    private Gamepad gamePad;

    [SerializeField]
    private Player[] player;

    [SerializeField]
    private RectTransform _turn;

    private int playerNumber = 2;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        gamePad = GetComponent<Gamepad>();
    }

    private void Update()
    {
        if(gamePad.buttonB.trigger)
        {
            player[0].Controll(true);
            player[1].Controll(false);
            playerNumber = 0;
        }
        else if (gamePad.buttonX.trigger)
        {
            player[0].Controll(false);
            player[1].Controll(true);
            playerNumber = 1;
        }
        else if(gamePad.buttonY.trigger)
        {
            player[0].Controll(true);
            player[1].Controll(true);
            playerNumber = 2;
        }
        // 選択されている色が表示されるように回転
        _turn.eulerAngles = new Vector3(0F, 0F, playerNumber * 120F);
    }

    private void isClear()
    {
        Debug.Log("Clear");
    }

    private void isFail(GameObject failObject)
    {
        failObject.GetComponent<Character>().Restart();
    }
}
