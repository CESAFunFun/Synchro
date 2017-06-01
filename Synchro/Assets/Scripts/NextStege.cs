using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStege : MonoBehaviour {

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        {
            SceneManager.LoadScene("Select");
        }
    }
}
