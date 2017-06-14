using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Child")
        {
            GameObject.Find("GameManager").SendMessage("Clear");
        }
    }
}
