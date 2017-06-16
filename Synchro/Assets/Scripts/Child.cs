using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    private ParticleSystem particle;
    protected override void Start()
    {
        base.Start();
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    protected override void Update()
    {
        base.Update();

        if(players[0].conectflag && players[1].conectflag)
        {
            if (players[0].conectflag)
            {
                downGravity = players[0].downGravity;
                transform.position = new Vector3(
                    players[0].transform.position.x,
                    players[0].transform.position.y,
                    this.transform.position.z);
            }

            if (players[1].conectflag)
            {
                downGravity = players[1].downGravity;
                transform.position = new Vector3(
                    players[1].transform.position.x,
                    players[1].transform.position.y,
                    this.transform.position.z);
            }

            particle.startRotation++;
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
