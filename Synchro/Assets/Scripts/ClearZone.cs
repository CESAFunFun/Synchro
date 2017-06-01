using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    bool clear = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Child")
        {
            clear = true;
            //GameManager.Instance.SendMessage("isClear");
        }
    }
    private void OnGUI()
    {
        if (clear)
        {
            GUI.Button(new Rect(Screen.width/2, 100, 450, 100), "Clear");
        }
    }
}
