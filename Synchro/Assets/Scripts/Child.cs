using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeadZone")
        {
            Restart();

            for(int i=0;i<players.Length;i++)
            {
                players[i].Restart();
            }
        }
    }
}
