﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Child")
        {
            GameManager.Instance.SendMessage("isClear");
        }
    }
}
