using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : Character {

    [SerializeField]
    private Player[] players;

    private ParticleSystem particle;
    private void Start()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        base.Update();

        if(players[0].conectflag && players[1].conectflag)
        {
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
