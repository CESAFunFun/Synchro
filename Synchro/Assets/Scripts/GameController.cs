using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int mapLevel = 0;

    [HideInInspector]
    public Gamepad gamepad;

    private static GameController _instance;

    public static GameController Instance { get { return _instance; } }

    private void Awake() {
        gamepad = GetComponent<Gamepad>();

        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
