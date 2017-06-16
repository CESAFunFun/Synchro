using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStege : MonoBehaviour {

    [SerializeField]
    private string _sceneName;
    private void Update()
    {
        if(GameController.Instance.gamepad.buttonA.trigger)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
