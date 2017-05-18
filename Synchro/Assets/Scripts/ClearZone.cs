using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    public bool isClear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Child")
        {
            isClear = true;
        }
    }
}
