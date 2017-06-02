﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Child")
        {
            GameObject.Find("GameManager").SendMessage("isFail", other.gameObject);
        }
    }
}
