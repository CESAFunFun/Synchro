using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool isFail = false; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Child")
        {
            isFail = true;
        }
    }
}
