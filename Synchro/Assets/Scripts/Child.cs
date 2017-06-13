using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    private Gamepad gamepad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            GameObject.Find("GameManager").SendMessage("Fail");
        }
    }
}
