using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    private Gamepad gamepad;
    private float rot = 0;
    private ParticleSystem par;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        gamepad = GameController.Instance.gamepad;
        par = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    protected override void Update()
    {
        base.Update();

        if (players[0].conectflag && players[1].conectflag)
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

            if(gamepad.leftButton.down)
            {
                BlinkPosition();
            }
            rot += 0.5f;
        }
        else
        {
            rot = 0;
        }

        par.startRotation = rot;


        
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
