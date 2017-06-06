using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStege : MonoBehaviour {

    private void Update()
    {
        if(GameManager.Instance.gamePad.startButton.trigger)
        {
            SceneManager.LoadScene("Select");
        }
    }
}
