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

    protected override void Update()
    {
        base.Update();

        if(players[0].conectflag && players[1].conectflag)
        {
            if (players[0].conectflag)
            {
                this.downGravity = players[0].downGravity;
                transform.position = new Vector3(
                    players[0].transform.position.x,
                    players[0].transform.position.y,
                    transform.position.z);
            }
            if (players[1].conectflag)
            {
                this.downGravity = players[1].downGravity;
                transform.position = new Vector3(
                    players[1].transform.position.x,
                    players[1].transform.position.y,
                    transform.position.z);
            }
        }
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
